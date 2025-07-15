using MongoDB.Driver;
using TypeToSearch.Domain.Interfaces.Repositories;
using TypeToSearch.Infrastructure.Contexts;

namespace TypeToSearch.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly MongoContext _context;

        public LogRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task InsertLogAsync<T>(T log, string collectionName, string databaseName)
        {
            var collection = _context.GetCollection<T>(collectionName, databaseName);
            await collection.InsertOneAsync(log);
        }

        public async Task<List<T>> FindLogsAsync<T>(FilterDefinition<T> filter, string collectionName, string databaseName)
        {
            var collection = _context.GetCollection<T>(collectionName, databaseName);
            return await collection.Find(filter).ToListAsync();
        }
    }
}