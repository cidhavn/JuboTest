using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JuboTest.Repository.Jubo
{
    [BsonIgnoreExtraElements]
    public abstract class JuboModelBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}