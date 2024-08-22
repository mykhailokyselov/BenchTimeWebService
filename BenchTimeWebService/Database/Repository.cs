using BenchTimeWebService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BenchTimeWebService.Database
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<T> _collection;

        public Repository(MongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> Update(string id, T entity)
        {
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", id), entity);
            return entity;
        }

        public async Task Delete(string id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public async Task Delete1(string id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
        }

        public async Task<T> Get(string id)
        {
            var entity = await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();
            return entity;
        }

        // Add more CRUD methods as needed...
    }
}
