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

		return services;
	}
}

public static class ApplicationServicesRegistration
{
	public static WebApplication RegisterApplicationServices(this WebApplication app)
	{
		app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

		app.MapCarter();

		return app;
	}
}
