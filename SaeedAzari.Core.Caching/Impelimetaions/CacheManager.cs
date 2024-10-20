using Microsoft.Extensions.Caching.Memory;
using SaeedAzari.Core.Caching.Interfaces;

namespace SaeedAzari.Core.Caching.Impelimetaions
{
    public class CacheManager<TEntry>(IMemoryCache cache, MemoryCacheEntryOptions memoryCacheEntryOptions) : ICacheManager<TEntry>
    {
        internal readonly string _cacheKey = typeof(TEntry).AssemblyQualifiedName?.ToLower() + typeof(ICacheManager<>).GetType().Name + typeof(TEntry).GetType().Name;
        public virtual async Task<TEntry> GetOrCreateAsync(Func<CancellationToken, Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default)
        {
            if (!cache.TryGetValue(_cacheKey, out TEntry _cachedItem))
            {
                _cachedItem = await cacheFunction(cancellationToken);
                cache.Set(_cacheKey, _cachedItem, memoryCacheEntryOptions);
            }
            if (_cachedItem == null)
            {
                _cachedItem = await cacheFunction(cancellationToken);
                cache.Set(_cacheKey, _cachedItem, memoryCacheEntryOptions);
            }
            return _cachedItem;
        }
        public virtual async Task<TEntry> GetOrCreateAsync(Func<Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default)
        {
            if (!cache.TryGetValue(_cacheKey, out TEntry _cachedItem))
            {
                _cachedItem = await cacheFunction();
                cache.Set(_cacheKey, _cachedItem, memoryCacheEntryOptions);
            }
            if (_cachedItem == null)
            {
                _cachedItem = await cacheFunction();
                cache.Set(_cacheKey, _cachedItem, memoryCacheEntryOptions);
            }
            return _cachedItem;
        }
        public virtual void ResetCache() => cache.Remove(_cacheKey);


    }

}
