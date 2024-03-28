using SaeedAzari.core.entities;

namespace SaeedAzari.Core.Services.Abstractions.Interface;

public interface IAuditEntityService<TKey, TEntity> : IEntityService<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IAbstractAuditEntity<TKey>
{
   
}

public interface IAuditEntityService<TEntity> : IAuditEntityService<Guid, TEntity>
    where TEntity : IAuditEntity
{

}