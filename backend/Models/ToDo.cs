using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class ToDo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("isDone")]
        public bool IsDone { get; set; }

        public ToDo(string description, bool isDone)
        {
            Description = description;
            IsDone = isDone;
        }

        public ToDo() {}
    }
}
