using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using TypeToSearch.Domain.Interfaces.Services;
using TypeToSearch.Infrastructure.ExternalServices;

namespace TypeToSearch.Infrastructure
{
    public static class ServiceInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //#region Contexts
            //services.AddSingleton<ContextMongoDB>();
            //services.AddScoped<RelacionalContext>();
            //#endregion

            //#region Repositories
            //services.AddScoped<ICobrancaHistoricoRepository, CobrancaHistoricoRepository>();
            //#endregion

            #region Services
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
            #endregion

            return services;
        }
    }
}
