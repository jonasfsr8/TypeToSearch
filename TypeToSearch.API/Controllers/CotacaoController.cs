using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TypeToSearch.Application.Dtos.Requests;
using TypeToSearch.Application.Services;

namespace TypeToSearch.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CotacaoController : ControllerBase
    {
        private readonly CotacaoService _cotacaoService;

        public CotacaoController(CotacaoService cotacaoService)
        {
            _cotacaoService = cotacaoService;
        }

        [HttpGet("Endereco/{cep}")]
        [SwaggerOperation(Summary = "Retorna endereço completo através do CEP")]
        public async Task<IActionResult> GetAddressAsync(string cep)
        {
            var result = await _cotacaoService.SearchAddressAsync(cep);

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("Combinacoes")]
        [SwaggerOperation(Summary = "Lista combinações disponíveis de cotação")]
        public async Task<IActionResult> CodeList()
        {
            var result = await _cotacaoService.GetAllCombinationsAsync();

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Pesquisar")]
        [SwaggerOperation(Summary = "Para obter as últimas taxas de câmbio [ Ex: code: USD - codeIn: BRL ]")]
        public async Task<IActionResult> GetQuoteAsync([FromBody] QuoteRequestDto request)
        {
            var result = await _cotacaoService.QuoteAsync(request);

            return StatusCode(result.StatusCode, result);
        }
    }
}
