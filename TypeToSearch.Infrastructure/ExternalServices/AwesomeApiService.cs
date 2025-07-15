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

        public Task<GenericResponse<CepResponseDto>> BuscarEnderecoAsync(string cep) =>
            ExecuteRequestAsync<CepResponseDto>($"{_cepUrl}/{cep}");

        public Task<GenericResponse<Dictionary<string, CotacaoResponseDto>>> CotacaoAsync(string code, string codeIn) =>
            ExecuteRequestAsync<Dictionary<string, CotacaoResponseDto>>($"{_currUrl}/last/{code}-{codeIn}");

        private async Task<GenericResponse<T>> ExecuteRequestAsync<T>(string url)
        {
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("x-api-key", _token);

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                var error = JsonSerializer.Deserialize<ErrorResponseDto>(response.Content);
                return new GenericResponse<T>
                {
                    Content = default,
                    Msg = error?.message ?? "Erro desconhecido",
                    StatusCode = (int)response.StatusCode
                };
            }

            var content = JsonSerializer.Deserialize<T>(response.Content);

            return new GenericResponse<T>
            {
                Content = content,
                Msg = "Requisição realizada com sucesso",
                StatusCode = (int)response.StatusCode
            };
        }
    }
}