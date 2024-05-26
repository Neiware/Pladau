using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PladauAPI.Data;
using PladauAPI.Models;

namespace PladauAPI.Services
{
    public class MongoDBService
    {
        private readonly MongoDbContext _context;
        public MongoDBService(MongoDbContext context)
        {
            _context = context;            
        }

        #region ADMIN CONTROLLER
        public async Task<List<T>> GetAllAsync<T>(string collectionName)
        {
            var collection = _context.GetCollection<T>(collectionName);
            return await collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(string collectionName, string id)
        {
            var collection = _context.GetCollection<T>(collectionName);
            return await collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync<T>(string collectionName, T document)
        {
            var collection = _context.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);
        }

        public async Task UpdateAsync<T>(string collectionName, string id, T document)
        {
            var collection = _context.GetCollection<T>(collectionName);
            await collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), document);
        }

        public async Task DeleteAsync<T>(string collectionName, string id)
        {
            var collection = _context.GetCollection<T>(collectionName);
            await collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public async Task UpdateAsync<T>(string collectionName, string id, UpdateDefinition<T> updateDefinition)
        {
            var collection = _context.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.UpdateOneAsync(filter, updateDefinition);
        }

        public async Task ReplaceAsync<T>(string collectionName, string id, T updatedDocument)
        {
            var collection = _context.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.ReplaceOneAsync(filter, updatedDocument);
        }
        #endregion

    }
}
