namespace SaeedAzari.Core.Caching.Interfaces
{
    public interface ICacheManager<TEntry>
    {
        Task<TEntry> GetOrCreateAsync(Func<Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default);
        Task<TEntry> GetOrCreateAsync(Func<CancellationToken, Task<TEntry>> cacheFunction, CancellationToken cancellationToken = default);
        void ResetCache();
    }
}
