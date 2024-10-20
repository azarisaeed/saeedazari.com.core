using Microsoft.Extensions.Caching.Memory;
using SaeedAzari.Core.Caching.Interfaces;

namespace SaeedAzari.Core.Caching.Impelimetaions
{
    public class MultiKeyCacheManager<TEntry>(IMemoryCache cache, MemoryCacheEntryOptions memoryCacheEntryOptions) : IMultiKeyCacheManager<TEntry>
    {
        internal readonly string _cacheKey = typeof(TEntry).AssemblyQualifiedName?.ToLower() +
            typeof(ICacheManager<>).GetType().Name + typeof(TEntry).GetType().Name;
        public virtual Task<TEntry> GetOrCreateAsync(Func<CancellationToken, Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default)
        {
            return GetOrCreateAsync(cacheFunction, _cacheKey, cancellationToken);
        }
        public virtual async Task<TEntry> GetOrCreateAsync(Func<CancellationToken, Task<TEntry>> cacheFunction, string key, CancellationToken cancellationToken = default)
        {
            if (!cache.TryGetValue(_cacheKey, out Dictionary<string, TEntry> dic))
            {
                var _cachedItem = await cacheFunction(cancellationToken);
                dic = new() { { key, _cachedItem } };
                cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
            }
            if (dic == null)
            {
                var _cachedItem = await cacheFunction(cancellationToken);
                dic = new() { { key, _cachedItem } };
                cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
            }

            if (!dic.TryGetValue(key, out TEntry? value))
            {
                var _cachedItem = await cacheFunction(cancellationToken);
                value = _cachedItem;
                dic.Add(key, value);
                cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
            }
            return value;
        }
        public virtual Task<TEntry> GetOrCreateAsync(Func<Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default)
        {
            return GetOrCreateAsync(cacheFunction, _cacheKey, cancellationToken);

        }
        public virtual async Task<TEntry> GetOrCreateAsync(Func<Task<TEntry>> cacheFunction, string key, CancellationToken cancellationToken = default)
        {

            if (!cache.TryGetValue(_cacheKey, out Dictionary<string, TEntry> dic))
            {
                var _cachedItem = await cacheFunction();
                dic = new() { { key, _cachedItem } };
                cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
            }
            if (dic == null)
            {
                var _cachedItem = await cacheFunction();
                dic = new() { { key, _cachedItem } };
                cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
            }

            if (!dic.TryGetValue(key, out TEntry? value))
            {
                var _cachedItem = await cacheFunction();
                value = _cachedItem;
                dic.Add(key, value);
                cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
            }
            return value;
        }
        public virtual void ResetCache()
        {
            cache.Remove(_cacheKey);
        }
        public virtual void ResetKey(string Key)
        {
            if (cache.TryGetValue(_cacheKey, out Dictionary<string, TEntry> dic))
                if (dic?.ContainsKey(Key) ?? false)
                {
                    dic.Remove(Key);
                    cache.Set(_cacheKey, dic, memoryCacheEntryOptions);
                }
        }


    }
}
