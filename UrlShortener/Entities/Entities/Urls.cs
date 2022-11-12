using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.Entities
{
    public class Urls
    {
        [BsonElement("id")]
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreateDate { get; set; }

    }

}
