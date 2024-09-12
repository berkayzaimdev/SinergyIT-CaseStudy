using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Models.Common;

public abstract class MongoEntity
{
	[BsonId]
	[BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.CSharpLegacy)]
	[BsonElement(Order = 0)]
	public Guid Id { get; set; }
}
