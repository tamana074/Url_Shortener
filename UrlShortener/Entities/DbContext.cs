using Entities;
using Microsoft.Extensions.Options; 
using MongoDB.Driver;

namespace DataAccess
{
    public class DbContext
    {
        internal IMongoDatabase MongoDatabase { get; set; }
        private IMongoClient _Client { get; set; }
        internal string CollectionName { get; set; }

        public DbContext(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _Client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            MongoDatabase = _Client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            CollectionName = mongoDBSettings.Value.CollectionName;
        }

    }
}
