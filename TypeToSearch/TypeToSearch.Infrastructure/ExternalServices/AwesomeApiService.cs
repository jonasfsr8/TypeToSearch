using RestSharp;
using System.Text.Json;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Interfaces.Services;
using TypeToSearch.Infrastructure.ExternalServices.Dtos.Responses;

namespace TypeToSearch.Infrastructure.ExternalServices
{
    public class AwesomeApiService : IAwesomeApiService
    {
        private readonly RestClient _client;
        private readonly string _cepUrl;
        private readonly string _currUrl;
        private readonly string _token;

        public AwesomeApiService(RestClient client, string cepUrl, string currUrl, string token)
        {
            _client = client;
            _cepUrl = cepUrl;
            _currUrl = currUrl;
            _token = token;
        }

        public async Task<GenericResponse<CepResponseDto>> BuscarEnderecoAsync(string cep)
        {
            var request = new RestRequest($"{_cepUrl}/{cep}", Method.Get);
            request.AddHeader("x-api-key", _token);

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                var error = JsonSerializer.Deserialize<ErrorResponseDto>(response.Content);

                return new GenericResponse<CepResponseDto>
                {
                    Content = null,
                    Msg = error.message,
                    StatusCode = (int)response.StatusCode
                };
            }

            var content = JsonSerializer.Deserialize<CepResponseDto>(response.Content);

            return new GenericResponse<CepResponseDto>
            {
                Content = content,
                Msg = "CEP encontrado com sucesso",
                StatusCode = (int)response.StatusCode
            };
        }

        public async Task<GenericResponse<Dictionary<string, CotacaoResponseDto>>> CotacaoAsync(string code, string codeIn)
        {
            var resquest = new RestRequest($"{_currUrl}/last/{code}-{codeIn}", Method.Get);
            resquest.AddHeader("x-api-key", _token);

            var response = await _client.ExecuteAsync(resquest);

            if (!response.IsSuccessful)
            {
                var error = JsonSerializer.Deserialize<ErrorResponseDto>(response.Content);

                return new GenericResponse<Dictionary<string, CotacaoResponseDto>>
                {
                    Content = null,
                    Msg = error.message,
                    StatusCode = (int)response.StatusCode
                };
            }

            var content = JsonSerializer.Deserialize<Dictionary<string, CotacaoResponseDto>>(response.Content);

            return new GenericResponse<Dictionary<string, CotacaoResponseDto>>
            {
                Content = content,
                Msg = "Cotação obtida com sucesso",
                StatusCode = (int)response.StatusCode
            };
        }
    }
}