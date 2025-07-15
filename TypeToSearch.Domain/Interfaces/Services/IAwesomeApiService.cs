using TypeToSearch.Domain.Dtos.Responses;

namespace TypeToSearch.Domain.Interfaces.Services
{
    public interface IAwesomeApiService
    {
        Task<GenericResponse<CepResponseDto>> BuscarEnderecoAsync(string cep);
        Task<GenericResponse<Dictionary<string, CotacaoResponseDto>>> CotacaoAsync(string code, string codeIn);
    }
}
