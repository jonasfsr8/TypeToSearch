using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TypeToSearch.Infrastructure.Context
{
    public class ContextMongoDB
    {
        private readonly IConfiguration _configuration;
        private IMongoClient _client;
        private IMongoDatabase _database;

        public ContextMongoDB(IConfiguration configuration)
        {
            _configuration = configuration;
            Initialize();
        }

        private void Initialize()
        {
            var connectionString = _configuration.GetConnectionString("InstanceMongoDB");
            _client = new MongoClient(connectionString);
        }

        //public IMongoCollection<dto> SessionLog
        //{
        //    get
        //    {
        //        var database = _client.GetDatabase("database");

        //        return database.GetCollection<dto>("collection");
        //    }
        //}

    }
}
