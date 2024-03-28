using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Services.Abstractions.Interface;

namespace SaeedAzari.Core.Services.Impeliments
{
    public class AuditEntityService<TKey, TEntity>(IAuditEntityRepository<TKey, TEntity> repository) : EntityService<TKey, TEntity>(repository), IAuditEntityService<TKey, TEntity>
       where TKey : IEquatable<TKey>
       where TEntity : IAbstractAuditEntity<TKey>
    {
    }

    public class AuditEntityService<TEntity>(IAuditEntityRepository<Guid, TEntity> repository) : AuditEntityService<Guid, TEntity>(repository), IEntityService<TEntity>
        where TEntity : IAuditEntity
    {
    }
}
