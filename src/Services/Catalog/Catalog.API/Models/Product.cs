using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    [BsonElement(Order = 0)]
    public Guid Id { get; set; }

	[BsonRepresentation(MongoDB.Bson.BsonType.String)]
	[BsonElement(Order = 1)]
	public string Name { get; set; } = default!;

	[BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
	[BsonElement(Order = 2)]
	public decimal Price { get; set; }
}
