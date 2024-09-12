namespace Catalog.API.Models;

public class Brand : MongoEntity, IMongoEntity
{
	[BsonRepresentation(MongoDB.Bson.BsonType.String)]
	[BsonElement(Order = 1)]
	public string Name { get; set; } = default!;
}
