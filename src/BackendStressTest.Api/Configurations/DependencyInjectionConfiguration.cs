using BackendStressTest.Infrastructure.CrossCutting.DI;

namespace BackendStressTest.Api.Configurations
{
    internal static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            DIFactory.ConfigureDI(services, configuration);
            return services;
        }
    }
}
