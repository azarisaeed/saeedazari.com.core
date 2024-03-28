

using SaeedAzari.core.Common;
using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Repositories.EF.Context;

namespace SaeedAzari.Core.Repositories.EF.Contexed
{
    public class EntityRepository<TKey, TEntity>(CoreDBContext efDbContext, IApplicationContext applicationContext) : EntityRepository<TKey, TEntity, CoreDBContext>(efDbContext, applicationContext)
      where TKey : IEquatable<TKey>
      where TEntity : IEntity<TKey>
    {
    }
    public class EntityRepository<TEntity>(CoreDBContext efDbContext, IApplicationContext applicationContext) : EntityRepository<Guid, TEntity, CoreDBContext>(efDbContext, applicationContext), IEntityRepository<TEntity>
      where TEntity : IEntity
    {
    }
}
