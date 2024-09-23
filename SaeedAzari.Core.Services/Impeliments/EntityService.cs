
using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Services.Abstractions.Interface;

namespace SaeedAzari.Core.Services.Impeliments
{
    public class EntityService<TKey, TEntity, TRepository>(TRepository repository) : IEntityService<TKey, TEntity>
      where TKey : IEquatable<TKey>
      where TEntity : IAbstractEntity<TKey>
      where TRepository : IEntityRepository<TKey, TEntity>

    {
        protected readonly TRepository Repository = repository;

        public virtual Task Delete(TKey id, CancellationToken cancellationToken = default) =>
            Repository.Delete(id, cancellationToken: cancellationToken);

        public virtual Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default) =>
            Repository.GetAll(cancellationToken);

        public virtual Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default) =>
            Repository.GetById(id, cancellationToken);

        public virtual Task<List<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default) =>
            Repository.GetByIds(ids, cancellationToken);

        public virtual Task Create(TEntity entity, CancellationToken cancellationToken = default) =>
            Repository.Create(entity, cancellationToken: cancellationToken);

        public virtual Task Update(TEntity entity, CancellationToken cancellationToken = default) =>
            Repository.Update(entity, cancellationToken: cancellationToken);

        public Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
            Repository.CreateMany(entities, cancellationToken: cancellationToken);

        public Task Delete(TEntity Entity, CancellationToken cancellationToken = default) =>
            Repository.Delete(Entity, cancellationToken: cancellationToken);

        public Task DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default) =>
            Repository.DeleteMany(ids, cancellationToken: cancellationToken);

        public Task DeleteMany(IEnumerable<TEntity> Entities, CancellationToken cancellationToken = default) =>
            Repository.DeleteMany(Entities, cancellationToken: cancellationToken);
    }

    public class EntityService<TEntity, TRepository> : EntityService<Guid, TEntity, TRepository>, IEntityService<TEntity>
        where TEntity : IEntity
        where TRepository : IEntityRepository<TEntity>
    {
        public EntityService(TRepository repository) : base(repository)
        {
        }
    }
}
