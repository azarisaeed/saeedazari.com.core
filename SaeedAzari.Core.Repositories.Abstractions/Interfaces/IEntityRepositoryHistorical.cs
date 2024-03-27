using SaeedAzari.core.entities;

namespace SaeedAzari.Core.Repositories.Abstractions.Interfaces
{
    public interface IEntityRepositoryHistorical<TKey, TEntity> : IEntityRepository<TKey, TEntity>
      where TKey : IEquatable<TKey>
      where TEntity : IAbstractEntity<TKey>
    {
        Task Create(TEntity entity, string actionName, CancellationToken cancellationToken = default);
        Task CreateMany(IEnumerable<TEntity> entities, string actionName, CancellationToken cancellationToken = default);
        Task Update(TEntity entity, string actionName, CancellationToken cancellationToken = default);
        Task Delete(TKey id, string actionName, CancellationToken cancellationToken = default);
        Task DeleteMany(IEnumerable<TKey> ids, string actionName, CancellationToken cancellationToken = default);
        Task Delete(TEntity Entity, string actionName, CancellationToken cancellationToken = default);
        Task DeleteMany(IEnumerable<TEntity> Entities, string actionName, CancellationToken cancellationToken = default);
    }
    public interface IEntityRepositoryHistorical<TEntity> : IEntityRepositoryHistorical<Guid, TEntity>
        where TEntity : IEntity
    {
    }
}
