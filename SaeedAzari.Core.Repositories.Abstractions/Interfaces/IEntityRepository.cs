using SaeedAzari.core.entities;
using SaeedAzari.core.entities.models;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using System.Linq.Expressions;
namespace SaeedAzari.Core.Repositories.Abstractions.Interfaces.ReadOnly

{
    public interface IEntityRepository<TKey, TEntity> : IRepository
        where TKey : IEquatable<TKey>
        where TEntity : IAbstractEntity<TKey>
{

    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<IListResult<TEntity>> Find(IBaseSearchModel<TKey, TEntity> SearchModel, CancellationToken cancellationToken = default);
    Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    IQueryable<TEntity> AsQueryable();
}
}
namespace SaeedAzari.Core.Repositories.Abstractions.Interfaces.RightOnly
{
    public interface IEntityRepository<TKey, TEntity> : IRepository
        where TKey : IEquatable<TKey>
        where TEntity : IAbstractEntity<TKey>
    {
        Task Create(TEntity entity, CancellationToken cancellationToken = default);
        Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task Update(TEntity entity, CancellationToken cancellationToken = default);
        Task Delete(TKey id, CancellationToken cancellationToken = default);
        Task DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default);
        Task Delete(TEntity Entity, CancellationToken cancellationToken = default);
        Task DeleteMany(IEnumerable<TEntity> Entities, CancellationToken cancellationToken = default);
    }
}

namespace SaeedAzari.Core.Repositories.Abstractions.Interfaces
{
   
    public interface IRepository
    {
    }
    public interface IEntityRepository<TKey, TEntity> : IRepository, ReadOnly.IEntityRepository<TKey, TEntity>, RightOnly.IEntityRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : IAbstractEntity<TKey>
    {

    }

    public interface IEntityRepository<TEntity> : IEntityRepository<Guid, TEntity>
        where TEntity : IEntity
    {
    }
}
