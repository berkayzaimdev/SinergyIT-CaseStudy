namespace Shared.Extensions;

public static class ServicesRegistration
{
	public static IServiceCollection AddRabbitMq
	(this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
	{
		services.AddMassTransit(config =>
		{
			config.SetKebabCaseEndpointNameFormatter();

			if (assembly != null)
				config.AddConsumers(assembly);

			config.UsingRabbitMq((context, configurator) =>
			{
				configurator.Host(new Uri(configuration["RabbitMq:Host"]!), host =>
				{
					host.Username(configuration["RabbitMq:UserName"]!);
					host.Password(configuration["RabbitMq:Password"]!);
				});
				configurator.ConfigureEndpoints(context);
			});
		});

		return services;
	}
}
