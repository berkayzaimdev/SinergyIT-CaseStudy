using Identity.API.Persistence;

namespace Identity.API.Extensions;

public static class ServicesRegistration
{
	public static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		var assembly = typeof(Program).Assembly;

		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(assembly);
		});

		services.AddCors();

		services.AddCarter();

		services.AddDbContext<IdentityContext>(opts => opts.UseNpgsql("..."));

		return services;
	}
}

public static class ApplicationServicesRegistration
{
	public static WebApplication RegisterApplicationServices(this WebApplication app)
	{
		app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

		app.MapCarter();

		app.Run();

		return app;
	}
}

