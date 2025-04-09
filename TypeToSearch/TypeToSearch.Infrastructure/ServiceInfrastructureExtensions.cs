using Microsoft.Extensions.DependencyInjection;
using TypeToSearch.Infrastructure.Context;

namespace TypeToSearch.Infrastructure
{
    public static class ServiceInfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            #region Contexts
            services.AddSingleton<ContextMongoDB>();
            services.AddScoped<RelacionalContext>();
            #endregion

            #region Repositories
            //services.AddScoped<ICobrancaHistoricoRepository, CobrancaHistoricoRepository>();
            #endregion

            return services;
        }
    }
}
