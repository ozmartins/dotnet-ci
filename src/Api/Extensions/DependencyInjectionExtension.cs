using Api.Infra;
using Domain.Infra;
using iParty.Data.Infra;
using Infra.Repositories;

namespace Api.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCustomDependencyInjection(this IServiceCollection services)
        {
            services.Scan(scan => scan.FromAssembliesOf(typeof(BusinessInjection), typeof(DataInjection), typeof(ApiInjection))
                                      .AddClasses()
                                      .AsImplementedInterfaces()
                                      .WithScopedLifetime());

            services.AddSingleton<DatabaseConfig>();

            return services;
        }
    }
}
