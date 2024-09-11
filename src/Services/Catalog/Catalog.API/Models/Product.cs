using Catalog.API.Models.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models;

public class Product : MongoEntity, IMongoEntity
{
	[BsonRepresentation(MongoDB.Bson.BsonType.String)]
	[BsonElement(Order = 1)]
	public string Name { get; set; } = default!;

	[BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
	[BsonElement(Order = 2)]
	public decimal Price { get; set; }
}
