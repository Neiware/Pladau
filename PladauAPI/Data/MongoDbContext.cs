using Microsoft.Extensions.Options;
using MongoDB.Driver; 
using PladauAPI.Models;
using System.Linq.Expressions;

namespace PladauAPI.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
         _database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }

    public async Task<List<T>> GetAsync<T>(string collectionName, Expression<Func<T, bool>> filterExpression)
    {
        var collection = _database.GetCollection<T>(collectionName);
        var result = await collection.Find(filterExpression).ToListAsync();
        return result;
    }
}
