using Dapper;
using TypeToSearch.Domain.Dtos.Requests;
using TypeToSearch.Domain.Entities;
using TypeToSearch.Domain.Interfaces.Repositories;
using TypeToSearch.Infrastructure.Contexts;
namespace TypeToSearch.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlContext _context;

        public UserRepository(SqlContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterAsync(RegisterUserRequestDto request)
        {
            using (var con = await _context.GetConnectionAsync("SqlConnection"))
            {
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        string query = @"INSERT INTO Users (NmUser, Login, PasswordHash)
                                        VALUES (@Name, @Login, @Password);
                                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                        var id = await con.ExecuteScalarAsync<int>(query, request, transaction);

                        transaction.Commit();
                        return id;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            using (var con = await _context.GetConnectionAsync("SqlConnection"))
            {
                string query = @"SELECT * FROM Users WHERE Login = @Email";

                var user = await con.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
                return user;
            }
        }
    }
}