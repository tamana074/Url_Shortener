using System.Linq.Expressions;
using Entities;
using Microsoft.Extensions.Options; 
using MongoDB.Driver;

namespace DataAccess
{
    public class MongoDBService<T>
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

        private readonly IMongoCollection<T> _urlsCollection;


        public async Task<List<T>> GetAsync()
        {
            return GetCollection<T>(_CollectionName).AsQueryable().ToList();
        }
        
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> where)
        {
            return GetCollection<T>(_CollectionName).AsQueryable().Where(where).ToList();
        }

        public async Task CreateAsync(T urls)
        {
            await _urlsCollection.InsertOneAsync(urls);
        }

    }
}
