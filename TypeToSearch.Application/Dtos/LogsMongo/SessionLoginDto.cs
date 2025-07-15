using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TypeToSearch.Application.Dtos.LogsMongo
{
    public class SessionLoginDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string userId { get; set; }
        public string ip { get; set; }
        public string userAgent { get; set; }
        public Info login { get; set; }
        public DateTime insertDate { get; set; }
    }

    public class Info
    {
        public string token { get; set; }
        public DateTime created { get; set; }
        public DateTime expires { get; set; }
    }
}