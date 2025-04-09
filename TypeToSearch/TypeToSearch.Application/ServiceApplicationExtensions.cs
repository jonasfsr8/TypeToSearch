using Microsoft.Extensions.DependencyInjection;

namespace TypeToSearch.Application
{
    public static class ServiceApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<ILoginService, LoginService>();
            //services.AddScoped<ILogService, LogService>();
            //services.AddScoped<INegociacaoService, NegociacaoService>();
            //services.AddScoped<IDashBoardService, DashBoardService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ICobrancaService, CobrancaService>();
            //services.AddScoped<IPaymentsService, PaymentsService>();

            //services.AddScoped<ITokenService>(provider =>
            //{
            //    var configuration = provider.GetRequiredService<IConfiguration>();
            //    var secretKey = configuration["JwtSettings:SecretKey"];
            //    var issuer = configuration["JwtSettings:Issuer"];
            //    var audience = configuration["JwtSettings:Audience"];

            //    return new TokenService(secretKey, issuer, audience);
            //});

            return services;
        }
    }
}
