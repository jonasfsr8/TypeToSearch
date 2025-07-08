using System.Text.RegularExpressions;
using TypeToSearch.Application.Dtos.Requests;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Interfaces.Services;

namespace TypeToSearch.Application.Services
{
    public class CotacaoService
    {
        private readonly IAwesomeApiService _awesomeApiService;

        public CotacaoService(IAwesomeApiService awesomeApiService)
        {
            _awesomeApiService = awesomeApiService;
        }

        public async Task<GenericResponse<CepResponseDto>> SearchAddressAsync(string zipcode)
        {
            var request = Regex.Replace(zipcode, "[^0-9]", "");

            var response = await _awesomeApiService.BuscarEnderecoAsync(request);

            return response;
        }

        public async Task<GenericResponse<Dictionary<string, CotacaoResponseDto>>> QuoteAsync(QuoteRequestDto request)
        {
            var response = await _awesomeApiService.CotacaoAsync(request.Code, request.CodeIn);

            return response;
        }
    }
}
