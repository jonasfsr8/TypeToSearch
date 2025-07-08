using System.Net;
using TypeToSearch.Application.Dtos.Requests;
using TypeToSearch.Domain.Dtos.Requests;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Exceptions;
using TypeToSearch.Domain.Interfaces.Repositories;
using TypeToSearch.Domain.Interfaces.Services;

namespace TypeToSearch.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _tokenService;

        public UserService(ITokenService tokenService, IUserRepository repository)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<GenericResponse<object>> CreateUserAsync(RegisterUserRequestDto request)
        {
            var existingUser = await _repository.GetByEmailAsync(request.Login);
            if (existingUser != null)
            {
                throw new ResourceAlreadyExistsException("Usuário já cadastrado.");
            }

            string pass = request.PasswordHash;

            request.PasswordHash = _tokenService.GenHashPassword(pass);

            int userId = await _repository.RegisterAsync(request);

            return new GenericResponse<object>
            {
                Content = userId,
                Msg = "Usuário criado com sucesso",
                StatusCode = 201
            };
        }

        public async Task<GenericResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var usuario = await _repository.GetByEmailAsync(request.Login);

            if (usuario is null || !_tokenService.VerifyHashPassword(usuario, request.Password))
                throw new UnauthorizedAccessException("credenciais de autenticação inválidas");

            var token = _tokenService.GenJwtToken(usuario);

            var created = DateTime.Now;
            var expires = created.AddHours(1);

            var response = new GenericResponse<LoginResponseDto>
            {
                Content = new LoginResponseDto
                {
                    Token = token,
                    Created = created.ToString("yyyy-MM-dd HH:mm:ss"),
                    Expires = expires.ToString("yyyy-MM-dd HH:mm:ss"),
                },
                Msg = "logado com sucesso!",
                StatusCode = (int)HttpStatusCode.OK
            };

            return response;
        }
    }
}
