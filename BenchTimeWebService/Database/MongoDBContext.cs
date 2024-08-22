namespace BenchTimeWebService.Database
{
    using BenchTimeWebService.Models;
    using MongoDB.Driver;

    public class MongoDbContext
    {
        private readonly MongoDB.Driver.IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            //var conn =  GetSection("MongoDB").GetValue<string>("ConnectionString");
            var connectionString = configuration["MongoDB:ConnectionString"];
            var databaseName = configuration["MongoDB:DatabaseName"];
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}
