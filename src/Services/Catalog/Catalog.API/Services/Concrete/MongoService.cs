namespace Catalog.API.Services.Concrete;

public class MongoService : IMongoService
{
	private readonly IMongoDatabase _database;

    public MongoService(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("MongoDB");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("MongoDB connection string is not configured");
        }

		try
		{
			MongoClient client = new(connectionString);
			_database = client.GetDatabase("Catalog");
		}
        catch
        {
			throw new MongoException("Failed to connect to MongoDB");
		}
    }

    public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
}
