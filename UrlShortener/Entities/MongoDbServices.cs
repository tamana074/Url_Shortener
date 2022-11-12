using System.Linq.Expressions;
using MongoDB.Driver;

namespace DataAccess;

public class MongoDbServices<T> where T : class
{
    private readonly DbContext _dbContext;

    public MongoDbServices(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _dbContext.MongoDatabase.GetCollection<T>(collectionName);
    }

    private readonly IMongoCollection<T> _urlsCollection;


    public async Task<List<T>> GetAsync()
    {
        return GetCollection<T>(_dbContext.CollectionName).AsQueryable().ToList();
    }
        
    public async Task<List<T>> GetAsync(Expression<Func<T, bool>> where)
    {
        return GetCollection<T>(_dbContext.CollectionName).AsQueryable().Where(where).ToList();
    }

    public async Task CreateAsync(T urls)
    {
        await _urlsCollection.InsertOneAsync(urls);
    }

}