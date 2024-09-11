using MongoDB.Driver;

namespace Catalog.API.Services.Abstract;

public interface IMongoService
{
	IMongoCollection<T> GetCollection<T>();
}
