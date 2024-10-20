namespace SaeedAzari.Core.Caching.Interfaces
{
    public interface IMultiKeyCacheManager<TEntry>
    {
        Task<TEntry> GetOrCreateAsync(Func<CancellationToken, Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default);
        Task<TEntry> GetOrCreateAsync(Func<CancellationToken, Task<TEntry>> cacheFunction, string key, CancellationToken cancellationToken = default);
        Task<TEntry> GetOrCreateAsync(Func<Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default);
        Task<TEntry> GetOrCreateAsync(Func<Task<TEntry>> cacheFunction, string key, CancellationToken cancellationToken = default);
        void ResetCache();
        void ResetKey(string Key);
    }
}
