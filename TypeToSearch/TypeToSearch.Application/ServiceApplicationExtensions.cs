using Microsoft.Extensions.DependencyInjection;
using TypeToSearch.Application.Services;

namespace TypeToSearch.Application
{
    public static class ServiceApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<CotacaoService>();

            return services;
        }
    }
}
