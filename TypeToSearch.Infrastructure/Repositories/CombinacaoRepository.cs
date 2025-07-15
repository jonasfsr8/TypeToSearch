using Dapper;
using TypeToSearch.Domain.Entities;
using TypeToSearch.Domain.Interfaces.Repositories;
using TypeToSearch.Infrastructure.Contexts;

namespace TypeToSearch.Infrastructure.Repositories
{
    public class CombinacaoRepository : ICombinacaoRepository
    {
        private readonly SqlContext _context;

        public CombinacaoRepository(SqlContext context) 
        {
            _context = context;
        }

        public async Task<List<Combinacao>> ListAsync()
        {
            using (var con = await _context.GetConnectionAsync("SqlConnection"))
            {
                var query = "SELECT * FROM Combinacoes;";

                var response = await con.QueryAsync<Combinacao>(query);
                return response.ToList();
            }
        }

        public async Task<bool> CheckCombination(string code)
        {
            using (var con = await _context.GetConnectionAsync("SqlConnection"))
            {
                var query = "SELECT 1 FROM Combinacoes WHERE Codigo = @Code";

                var result = await con.QueryFirstOrDefaultAsync<int?>(query, new { Code = code });

                return result.HasValue;
            }
        }
    }
}
