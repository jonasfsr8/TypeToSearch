using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TypeToSearch.Application.Dtos;
using TypeToSearch.Application.Services;

namespace TypeToSearch.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class CotacaoController : ControllerBase
    {
        private readonly CotacaoService _service;

        public CotacaoController(CotacaoService service)
        {
            _service = service;
        }

        [HttpGet("/GetAddress/{zipcode}")]
        [SwaggerOperation(Summary = "Retorna endereço completo através do CEP")]
        public async Task<IActionResult> GetAddressAsync(string zipcode)
        {
            var result = await _service.SearchAddressAsync(zipcode);

            if (result.Content == null)
                return StatusCode(result.StatusCode, result.Msg);

            return Ok(result);
        }

        [HttpPost("/Quote")]
        [SwaggerOperation(Summary = "Retorna cotação entre duas moedas")]
        public async Task<IActionResult> GetQuoteAsync([FromBody] QuoteRequestDto request)
        {
            var result = await _service.QuoteAsync(request);

            if (result.Content == null)
                return StatusCode(result.StatusCode, result.Msg);

            return Ok(result);
        }
    }
}
