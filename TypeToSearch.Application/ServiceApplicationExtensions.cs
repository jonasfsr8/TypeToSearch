using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypeToSearch.Application.Services;
using TypeToSearch.Domain.Interfaces.Services;

namespace TypeToSearch.Application
{
    public static class ServiceApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<CotacaoService>();
            services.AddScoped<UserService>();
            services.AddScoped<LogService>();
            services.AddScoped<ITokenService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var key = configuration["JwtSettings:secretkey"];
                var iss = configuration["JwtSettings:issuer"];
                var au = configuration["JwtSettings:audience"];

                return new TokenService(key, iss, au);
            });

            return services;
        }
    }
}
