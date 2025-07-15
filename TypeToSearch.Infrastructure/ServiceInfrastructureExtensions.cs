using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TypeToSearch.Domain.Interfaces.Repositories;
using TypeToSearch.Domain.Interfaces.Services;
using TypeToSearch.Infrastructure.Contexts;
using TypeToSearch.Infrastructure.ExternalServices;
using TypeToSearch.Infrastructure.Repositories;

namespace TypeToSearch.Infrastructure
{
    public static class ServiceInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();
            services.AddScoped<SqlContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICombinacaoRepository, CombinacaoRepository>();
            services.AddScoped<ILogRepository, LogRepository>();

            services.AddScoped<IAwesomeApiService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var cepUrl = configuration["EndPoints:Awesomeapi:cepUrl"];
                var currUrl = configuration["EndPoints:Awesomeapi:currUrl"];
                var token = configuration["EndPoints:Awesomeapi:token"];
                var options = new RestClientOptions();
                var client = new RestClient(options);

                return new AwesomeApiService(client, cepUrl, currUrl, token);
            });

            return services;
        }
    }
}
