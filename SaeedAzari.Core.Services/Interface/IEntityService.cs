using SaeedAzari.core.entities;

namespace SaeedAzari.Core.Services.Abstractions.Interface;

public interface IEntityService<TKey, TEntity> 
    where TKey : IEquatable<TKey>
    where TEntity : IAbstractEntity<TKey>
{
    Task Create(TEntity entity, CancellationToken cancellationToken = default);
    Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    Task Update(TEntity entity, CancellationToken cancellationToken = default);
    Task Delete(TKey id, CancellationToken cancellationToken = default);
    Task Delete(TEntity Entity, CancellationToken cancellationToken = default);

    Task DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
    Task DeleteMany(IEnumerable<TEntity> Entities, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
}

public interface IEntityService<TEntity> : IEntityService<Guid, TEntity>
    where TEntity : IEntity
{

}