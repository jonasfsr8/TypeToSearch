using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Entities;

namespace TypeToSearch.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenJwtToken(User user);
        string GenHashPassword(string password);
        bool VerifyHashPassword(User user, string password);
        GenericResponse<object> ValidateJwtToken(string token);
    }
}
