using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TypeToSearch.Infrastructure.Contexts
{
    public class MongoContext
    {
        private readonly IConfiguration _configuration;
        private IMongoClient _client;
        private IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Initialize();
        }

        private void Initialize()
        {
            var connectionString = _configuration.GetConnectionString("MongoConnection");
            _client = new MongoClient(connectionString);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName, string databaseName)
        {
            var database = _client.GetDatabase(databaseName);
            return database.GetCollection<T>(collectionName);
        }
    }
}