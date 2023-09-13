using BackendStressTest.Application;
using BackendStressTest.Application.Implementation;
using BackendStressTest.Extensions;
using BackendStressTest.Infrastructure.Data;
using BackendStressTest.Infrastructure.Data.Repositories;
using BackendStressTest.Infrastructure.Data.Repositories.Implementation;
using BackendStressTest.Infrastructure.Data.UnitOfWork;
using BackendStressTest.Services;
using BackendStressTest.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace BackendStressTest.Infrastructure.CrossCutting.DI
{
    public static class DIFactory
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            #region Host Layer
            #endregion

            #region Application Layer

            services.AddScoped<IPersonApplicationService, PersonApplicationService>();

            services.AddScoped<IMapperApplicationExtensionService, MapperApplicationExtensionService>();

            #endregion

            #region Services Layer

            services.AddScoped<IPersonService, PersonService>();

            #endregion

            #region Infrastructure Layer

            services.AddScoped(_ => new DbSession(configuration.GetConnectionString("BackendStressTestDb")!));
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPersonRepository, PersonRepository>();

            #endregion

            #region 
            #endregion

            #region 
            #endregion

            #region 
            #endregion
        }

    }
}
