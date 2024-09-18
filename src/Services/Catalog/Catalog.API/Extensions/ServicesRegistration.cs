namespace Catalog.API.Extensions;

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

		services.AddSingleton<IMongoService, MongoService>();

		services = SeedData(services).Result;

		return services;
	}

	private static async Task<IServiceCollection> SeedData(this IServiceCollection services)
	{
		using var scope = services.BuildServiceProvider().CreateScope();

		var mongoDbService = scope.ServiceProvider.GetRequiredService<IMongoService>() ?? throw new MongoException("Failed to connect to MongoDB");

		if (!await (await mongoDbService.GetCollection<Brand>().FindAsync(x => true)).AnyAsync())
		{
			var brandFaker = new Faker<Brand>()
				.RuleFor(b => b.Id, f => f.Random.Guid())
				.RuleFor(b => b.Name, f => f.Company.CompanyName());

			var brands = brandFaker.Generate(5);

			await mongoDbService.GetCollection<Brand>().InsertManyAsync(brands);

			if (!await (await mongoDbService.GetCollection<Product>().FindAsync(x => true)).AnyAsync())
			{
				var productFaker = new Faker<Product>()
				.RuleFor(p => p.Id, f => f.Random.Guid())
				.RuleFor(p => p.Name, f => f.Commerce.ProductName())
				.RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()))
				.RuleFor(p => p.BrandId, f => f.PickRandom(brands).Id);

				var products = productFaker.Generate(50);

				await mongoDbService.GetCollection<Product>().InsertManyAsync(products);

				if (!await (await mongoDbService.GetCollection<Stock>().FindAsync(x => true)).AnyAsync())
				{
					var stocks = products.Select(p => new Stock
					{
						Id = p.Id,
						Count = (int) new Random().NextInt64(1,100)
					}).ToList();

					await mongoDbService.GetCollection<Stock>().InsertManyAsync(stocks);
				}
			}
		}

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
