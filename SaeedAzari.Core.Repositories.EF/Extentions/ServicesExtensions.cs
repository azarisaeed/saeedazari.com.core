
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
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));
            services.AddScoped(typeof(IAuditEntityRepository<>), typeof(AuditEntityRepository<>));
            services.AddScoped(typeof(IAuditEntityRepository<,>), typeof(AuditEntityRepository<,>));
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

            services.AddScoped<TSqlserverDbContext>();
            services.AddScoped<CoreDBContext, TSqlserverDbContext>();
            return services;

        }
    }


}
