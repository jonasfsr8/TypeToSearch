using TypeToSearch.Domain.Dtos.Requests;
using TypeToSearch.Domain.Entities;

namespace TypeToSearch.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> RegisterAsync(RegisterUserRequestDto request);
        Task<User> GetByEmailAsync(string email);
    }
}
