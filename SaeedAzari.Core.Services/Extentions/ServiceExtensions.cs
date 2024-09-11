using SaeedAzari.Core.Services.Abstractions.Interface;
using SaeedAzari.Core.Services.Impeliments.Contexed;

namespace Microsoft.Extensions.DependencyInjection

{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterBaseServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped(typeof(IEntityService<,>), typeof(EntityService<,>));

            return services;
        }
    }
}
