

using Microsoft.Extensions.Caching.Memory;
using SaeedAzari.Core.Caching.Impelimetaions;
using SaeedAzari.Core.Caching.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class CacheManagerExtention
{

    public static IServiceCollection AddCaching(this IServiceCollection services, Action<MemoryCacheOptions> setupAction, MemoryCacheEntryOptions memoryCacheEntryOptions)
    {
        services.AddMemoryCache(setupAction);
        AddBaseCaching(services, memoryCacheEntryOptions);
        return services; ;
    }

    private static void AddBaseCaching(IServiceCollection services, MemoryCacheEntryOptions memoryCacheEntryOptions)
    {
        services.AddSingleton(typeof(ICacheManager<>), typeof(CacheManager<>));
        services.AddSingleton(typeof(IMultiKeyCacheManager<>), typeof(MultiKeyCacheManager<>));
        services.AddSingleton(i => memoryCacheEntryOptions);
    }

    public static IServiceCollection AddCaching(this IServiceCollection services, Action<MemoryCacheOptions> setupAction)
    {
        services.AddMemoryCache(setupAction);
        AddBaseCaching(services, GetMemoryCacheEntryOptions());
        return services;
    }
    /// <summary>
    ///  Get GetMemoryCacheEntryOptions for caching
    /// </summary>
    /// <param name="SlidingExpiration"> default is 1 hour</param>
    /// <param name="AbsoluteExpiration">default is 1 day</param>
    /// <returns></returns>
    public static MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TimeSpan? SlidingExpiration = null, TimeSpan? AbsoluteExpiration = null)
    {
        if (SlidingExpiration == null)
            SlidingExpiration = TimeSpan.FromHours(1);
        if (AbsoluteExpiration == null)
            AbsoluteExpiration = TimeSpan.FromDays(1);
        MemoryCacheEntryOptions s = new()
        {
            SlidingExpiration = SlidingExpiration,
            AbsoluteExpirationRelativeToNow = AbsoluteExpiration
        };

        return s;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services)
    {
        services.AddMemoryCache();
        AddBaseCaching(services, GetMemoryCacheEntryOptions());
        return services;
    }

}
