using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Identity.API.Extensions;

public static class ServicesRegistration
{
	public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		var assembly = typeof(Program).Assembly;

		services.RegisterDatabase(configuration);

		services.RegisterJwt(configuration);

		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(assembly);
		});

		services.AddCarter();

		services.AddCors();

		return services;
	}

	private static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("IdentityDb");

		if (string.IsNullOrEmpty(connectionString))
		{
			throw new ArgumentException("IdentityDb connection string is not configured");
		}

		services.AddIdentityCore<ApplicationUser>()
			.AddEntityFrameworkStores<IdentityContext>();

		services.AddDbContext<IdentityContext>(opts => opts.UseNpgsql(connectionString));

		return services;
	}

	private static IServiceCollection RegisterJwt(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<ITokenService, TokenService>();

		services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(o =>
		{
			var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? throw new ArgumentException("JWT settings are not configured");

			o.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = jwtSettings.Issuer,
				ValidAudience = jwtSettings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true
			};
		});

		return services;
	}
}

public static class ApplicationServicesRegistration
{
	public static WebApplication RegisterApplicationServices(this WebApplication app)
	{
		app.ApplyMigration();

		app.MapCarter();

		app.UseAuthentication();

		app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

		return app;
	}

	private static IApplicationBuilder ApplyMigration(this IApplicationBuilder app)
	{
		using var scope = app.ApplicationServices.CreateScope();

		using var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();

		db.Database.Migrate();

		return app;
	}
}

