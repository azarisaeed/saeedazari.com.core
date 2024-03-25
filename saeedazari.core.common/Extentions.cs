using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SaeedAzari.core.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IConfigurationBuilder AddJsonConfig(this IConfigurationBuilder configBuilder, IHostEnvironment env, string configFolder = "\\")
        {
            var folderName = env.ContentRootPath + configFolder;

            // setting base path for config folder
            configBuilder.SetBasePath(folderName);

            foreach (var filename in Directory.GetFiles(folderName))
            {
                // read only json files
                if (Path.GetExtension(filename) != ".json")
                    continue;

                // accept only root config files
                if (Path.GetFileName(filename).Split(".").Length != 2)
                    continue;

                // adding config file
                configBuilder.AddJsonFile(filename, optional: true, reloadOnChange: true)
                  .AddJsonFile($"{Path.GetFileNameWithoutExtension(filename)}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            }

            return configBuilder;
        }

        public static T? GetInstance<T>(this IConfiguration configuration, string sectionName) => configuration.GetSection(sectionName).Get<T>();
        public static IServiceCollection AddApplicationContext(this IServiceCollection services)
        {
            return AddApplicationContext<ApplicationContext>(services);
        }
        public static IServiceCollection AddApplicationContext<TApplicationContext>(this IServiceCollection services)
            where TApplicationContext : class, IApplicationContext
        {
            services.AddScoped<IApplicationContext, TApplicationContext>();
            return services;
        }

    }
}
