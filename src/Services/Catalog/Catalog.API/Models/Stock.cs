namespace Catalog.API.Models;

public class Stock : MongoEntity, IMongoEntity
{
	[BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
	[BsonElement(Order = 1)]
	public int Count { get; set; } = default!;
}
