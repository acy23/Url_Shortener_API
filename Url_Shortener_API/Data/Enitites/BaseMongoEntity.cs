using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Url_Shortener_API.Data.Enitites
{
    public class BaseMongoEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; } = ObjectId.GenerateNewId().ToString();
        public bool IsDeleted { get; set; } = false;
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
