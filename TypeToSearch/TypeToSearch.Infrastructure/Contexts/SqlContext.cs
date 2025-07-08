using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TypeToSearch.Infrastructure.Contexts
{
    public class SqlContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public SqlContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SqlConnection> GetConnectionAsync(string nomeInstancia)
        {
            _connection = new SqlConnection(_configuration.GetConnectionString(nomeInstancia));

            try
            {
                await _connection.OpenAsync();
            }
            catch (Exception ex)
            {
                Dispose();
                throw;
            }

            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}