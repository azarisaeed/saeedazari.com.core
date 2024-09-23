using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Services.Abstractions.Interface;

namespace SaeedAzari.Core.Services.Impeliments.Contexed
{
    public class EntityService<TKey, TEntity>(IEntityRepository<TKey, TEntity> repository) : EntityService<TKey, TEntity, IEntityRepository<TKey, TEntity>>(repository)
        where TKey : IEquatable<TKey>
        where TEntity : IAbstractEntity<TKey>
    {

    }

    public class EntityService<TEntity>(IEntityRepository<TEntity> repository) : EntityService<Guid, TEntity>(repository), IEntityService<TEntity>
        where TEntity : IEntity
    {
    }
}
