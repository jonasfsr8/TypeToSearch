using System.Text.RegularExpressions;
using TypeToSearch.Application.Dtos.Requests;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Entities;
using TypeToSearch.Domain.Exceptions;
using TypeToSearch.Domain.Interfaces.Repositories;
using TypeToSearch.Domain.Interfaces.Services;

namespace TypeToSearch.Application.Services
{
    public class CotacaoService
    {
        private readonly IAwesomeApiService _awesomeApiService;
        private readonly ICombinacaoRepository _combinacaoRepository;

        public CotacaoService(IAwesomeApiService awesomeApiService, ICombinacaoRepository combinacaoRepository)
        {
            _awesomeApiService = awesomeApiService;
            _combinacaoRepository = combinacaoRepository;
        }

        public async Task<GenericResponse<CepResponseDto>> SearchAddressAsync(string zipcode)
        {
            var request = Regex.Replace(zipcode, "[^0-9]", "");

            var response = await _awesomeApiService.BuscarEnderecoAsync(request);

            return response;
        }

        public async Task<GenericResponse<List<Combinacao>>> GetAllCombinationsAsync()
        {
            var response = await _combinacaoRepository.ListAsync();

            return new GenericResponse<List<Combinacao>>
            {
                Content = response,
                Msg = "Lista recuperada com sucesso!",
                StatusCode = 200,
            };
        }

        public async Task<GenericResponse<Dictionary<string, CotacaoResponseDto>>> QuoteAsync(QuoteRequestDto request)
        {
            await CheckCombinationAsync(request);

            var response = await _awesomeApiService.CotacaoAsync(request.Code, request.CodeIn);

            return response;
        }

        private async Task CheckCombinationAsync(QuoteRequestDto request)
        {
            var code = $"{request.Code.ToUpper()}-{request.CodeIn.ToUpper()}";

            var hasCombination = await _combinacaoRepository.CheckCombination(code);

            if (!hasCombination)
                throw new NotFoundException("Consulta inválida. Utilize o endpoint [GET /combinations] para consultar as combinações disponíveis.");
        }
    }
}
