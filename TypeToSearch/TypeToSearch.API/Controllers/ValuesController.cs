using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TypeToSearch.Domain.Dtos.Responses;

namespace TypeToSearch.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [Authorize]
        [HttpGet("Get")]
        [SwaggerOperation(Summary = "")]
        public async Task<IActionResult> Login([FromQuery] int CdUser)
        {
            return Ok(new { CdUser });
        }

        //[HttpPost("CreateUser")]
        //[SwaggerOperation(Summary = "")]
        //public async Task<IActionResult> CreateUser([FromBody] dt request)
        //{
        //    var response = await _userService.CreateUserAsync(request);
        //    return StatusCode(response.StatusCode, response);
        //}
    }
}
