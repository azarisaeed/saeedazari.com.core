
using SaeedAzari.Core.Enums;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoriesExtensions
    {
        public static IServiceCollection AddEnumDescriptor(this IServiceCollection services)
        {

            services.AddScoped(typeof(Descriptor<>), typeof(Descriptor<>));

            return services;

        }
    }
}
