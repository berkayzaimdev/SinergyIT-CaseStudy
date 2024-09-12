namespace Catalog.API.Models;

public class Product : MongoEntity, IMongoEntity
{
	[BsonRepresentation(MongoDB.Bson.BsonType.String)]
	[BsonElement(Order = 1)]
	public string Name { get; set; } = default!;

	[BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
	[BsonElement(Order = 2)]
	public decimal Price { get; set; }

	[BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.CSharpLegacy)]
	[BsonElement(Order = 3)]
	public decimal BrandId { get; set; }
}
