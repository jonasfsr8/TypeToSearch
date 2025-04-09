using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeToSearch.Infrastructure.Context
{
    public class RelacionalContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        public RelacionalContext(IConfiguration configuration)
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
