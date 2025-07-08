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

        //public IMongoCollection<SessionLogDto> SessionLog
        //{
        //    get
        //    {
        //        var database = _client.GetDatabase("Session");

        //        return database.GetCollection<SessionLogDto>("logStatus");
        //    }
        //}
    }
}
