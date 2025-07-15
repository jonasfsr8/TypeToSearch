using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TypeToSearch.Application.Dtos.Requests;
using TypeToSearch.Application.Services;
using TypeToSearch.Domain.Dtos.Requests;

namespace TypeToSearch.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("Registrar")]
        [SwaggerOperation(Summary = "Cadastro de usuário")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            var result = await _userService.CreateUserAsync(request);

            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Logar com um usuário")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = Request.Headers["User-Agent"].ToString();
            var result = await _userService.LoginAsync(request, ip, userAgent);

            return StatusCode(result.StatusCode, result);
        }
    }
}
