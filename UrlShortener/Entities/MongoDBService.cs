using DataAccess.Entities;
using Entities;
using Microsoft.Extensions.Options; 
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core;

namespace DataAccess
{
    public class MongoDBService
    {
        private IMongoDatabase _MongoDatabase { get; set; }
        private IMongoClient _Client { get; set; }
        private string _CollectionName { get; set; }

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            _Client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            _MongoDatabase = _Client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _CollectionName = mongoDBSettings.Value.CollectionName;
        } 
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _MongoDatabase.GetCollection<T>(collectionName);
        }

        private readonly IMongoCollection<Urls> _urlsCollection;


        public async Task<List<Urls>> GetAsync()
        {
            return GetCollection<Urls>(_CollectionName).AsQueryable().ToList();
        }

        public async Task CreateAsync(Urls urls)
        {
            await _urlsCollection.InsertOneAsync(urls);
        }

    }
}
