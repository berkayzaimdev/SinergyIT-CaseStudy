namespace Basket.API.Extensions;

public static class ServicesRegistration
{
	public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		var assembly = typeof(Program).Assembly;

		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(assembly);
		});

		services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = configuration.GetConnectionString("Redis");
		});

		services.AddCors();

		services.AddCarter();

		services.RegisterJwt(configuration);

		services.AddControllers();

		services.AddHttpContextAccessor();

		services.AddRabbitMq(configuration, assembly);

		return services;
	}

	private static IServiceCollection RegisterJwt(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

		services.AddAuthorization();

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
		app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

		app.UseRouting();

		app.MapCarter();

		app.MapControllers();

		app.UseAuthentication();

		app.UseAuthorization();

		return app;
	}
}
