using TypeToSearch.Application.Dtos.LogsMongo;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Interfaces.Repositories;

namespace TypeToSearch.Application.Services
{
    public class LogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository) 
        {
            _logRepository = logRepository;
        }

        public async Task RegisterLoginAsync(int id, LoginResponseDto login, string ip, string userAgent)
        {
            try
            {
                var log = new SessionLoginDto
                {
                    userId = id.ToString(),
                    ip = ip,
                    userAgent = userAgent,
                    login = new Info
                    {
                        token = login.Token,
                        created = login.Created,
                        expires = login.Expires,
                    },
                    insertDate = DateTime.UtcNow,
                };

                await _logRepository.InsertLogAsync(log, "session_login", "logs");
            }
            catch { }
        }
    }
}
