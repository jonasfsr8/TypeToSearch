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
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly LogService _logRepository;

        public UserService(ITokenService tokenService, IUserRepository userRepository, LogService logRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _logRepository = logRepository;
        }

        public async Task<GenericResponse<int>> CreateUserAsync(RegisterUserRequestDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Login);
            if (existingUser != null)
            {
                throw new ResourceAlreadyExistsException("Usuário já cadastrado.");
            }

            var hashedPassword = _tokenService.GenHashPassword(request.Password);
            int userId = await _userRepository.RegisterAsync(new RegisterUserRequestDto
            {
                Name = request.Name,
                Login = request.Login,
                Password = hashedPassword
            });

            return new GenericResponse<int>
            {
                Content = userId,
                Msg = "Usuário criado com sucesso",
                StatusCode = 201
            };
        }

        public async Task<GenericResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request, string ip, string userAgent)
        {
            var usuario = await _userRepository.GetByEmailAsync(request.Login);

            if (usuario is null || !_tokenService.VerifyHashPassword(usuario, request.Password))
                throw new UnauthorizedAccessException("Credenciais de autenticação inválidas");

            var token = _tokenService.GenJwtToken(usuario);

            var response = new GenericResponse<LoginResponseDto>
            {
                Content = new LoginResponseDto
                {
                    Token = token,
                    Created = DateTime.Now,
                    Expires = DateTime.Now.AddHours(1),
                },
                Msg = "Logado com sucesso!",
                StatusCode = (int)HttpStatusCode.OK
            };

            await _logRepository.RegisterLoginAsync(usuario.CdUser, response.Content, ip, userAgent);

            return response;
        }

        
    }
}