using Identity.API.Models;
using Identity.API.Persistence;

namespace Identity.API.Extensions;

public static class ServicesRegistration
{
	public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		var assembly = typeof(Program).Assembly;

		string? connectionString = configuration.GetConnectionString("IdentityDb");

        if (string.IsNullOrEmpty(connectionString))
        {
			throw new ArgumentException("IdentityDb connection string is not configured");
		}

		services.AddIdentityCore<ApplicationUser>()
			.AddEntityFrameworkStores<IdentityContext>();

		services.AddDbContext<IdentityContext>(opts => opts.UseNpgsql(connectionString));

		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(assembly);
		});

		//services.AddCarter();

		services.AddCors();

		return services;
	}
}

public static class ApplicationServicesRegistration
{
	public static WebApplication RegisterApplicationServices(this WebApplication app)
	{
		app.ApplyMigration();

		//app.MapCarter();

		app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

		app.Run();

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

