using MongoDB.Driver;

namespace TypeToSearch.Domain.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task InsertLogAsync<T>(T log, string collectionName, string databaseName);
        Task<List<T>> FindLogsAsync<T>(FilterDefinition<T> filter, string collectionName, string databaseName);
    }
}