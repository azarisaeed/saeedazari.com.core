

using SaeedAzari.core.Common;
using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Repositories.EF.Context;

namespace SaeedAzari.Core.Repositories.EF.Contexed
{
    public class AuditEntityRepository<TKey, TEntity>(CoreDBContext efDbContext, IApplicationContext applicationContext) : EntityRepository<TKey, TEntity>(efDbContext, applicationContext), IEntityRepository<TKey, TEntity>
         where TEntity : IAuditEntity<TKey>
         where TKey : IEquatable<TKey>
    {



    }
    public class AuditEntityRepository<TEntity>(CoreDBContext efDbContext, IApplicationContext applicationContext) : AuditEntityRepository<Guid, TEntity>(efDbContext, applicationContext), IEntityRepository<TEntity>
       where TEntity : IAuditEntity

    {

    }


}
