
using Microsoft.Extensions.Configuration;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Repositories.EF.Contexed;
using SaeedAzari.Core.Repositories.EF.Context;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddSqlServer<TSqlserverDbContext>(this IServiceCollection services, string connectionString)
          where TSqlserverDbContext : CoreDBContext
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return new Option<CoreDBContext>(configuration.GetConnectionString(connectionString));
            });
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return new Option<TSqlserverDbContext>(configuration.GetConnectionString(connectionString));
            });
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));

            services.AddTransient<TSqlserverDbContext>();
            services.AddTransient<CoreDBContext, TSqlserverDbContext>();
            return services;

        }
    }


}
