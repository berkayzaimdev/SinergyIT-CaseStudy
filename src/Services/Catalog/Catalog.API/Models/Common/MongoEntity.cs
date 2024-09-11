using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models.Common;

public abstract class MongoEntity
{
	[BsonId]
	[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
	[BsonElement(Order = 0)]
	public Guid Id { get; set; }
}
