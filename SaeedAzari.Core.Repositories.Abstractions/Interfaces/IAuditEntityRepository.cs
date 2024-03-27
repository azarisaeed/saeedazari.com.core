using SaeedAzari.core.entities;

namespace SaeedAzari.Core.Repositories.Abstractions.Interfaces
{
    public interface IAuditEntityRepository<TKey, TEntity> : IEntityRepository<TKey, TEntity>
       where TKey : IEquatable<TKey>
       where TEntity : IAbstractAuditEntity<TKey>
    {
    }
    public interface IAuditEntityRepository<TEntity> : IAuditEntityRepository<Guid, TEntity>
    where TEntity : IAuditEntity
    {
    }
}
