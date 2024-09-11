using SaeedAzari.core.Common;
using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.Abstractions.Extensions;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.Core.Repositories.EF.Context;
namespace SaeedAzari.Core.Repositories.EF
{
    public class AuditEntityRepository<TKey, TEntity, TContext>(TContext efDbContext, IApplicationContext applicationContext) : EntityRepository<TKey, TEntity, TContext>(efDbContext, applicationContext), IEntityRepository<TKey, TEntity>
         where TEntity : IAuditEntity<TKey>
         where TKey : IEquatable<TKey>
         where TContext : CoreDBContext
    {
        public override Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity = entity.SetPropertiesOnCreate<TKey, TEntity>(ApplicationContext);
            return base.Create(entity, cancellationToken);
        }

        public override Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {

            entities = entities.SetPropertiesOnCreate<TKey, TEntity>(ApplicationContext);

            return base.CreateMany(entities, cancellationToken);
        }

        public override Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity = entity.SetPropertiesOnUpdate<TKey, TEntity>(ApplicationContext);
            return base.Update(entity, cancellationToken);
        }


    }
    public class AuditEntityRepository<TEntity, TContext>(TContext efDbContext, IApplicationContext applicationContext) : AuditEntityRepository<Guid, TEntity, TContext>(efDbContext, applicationContext), IEntityRepository<TEntity>
       where TEntity : IAuditEntity
                 where TContext : CoreDBContext

    {
        public override Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Id = new Guid();
            return base.Create(entity, cancellationToken);
        }

        public override Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {

            foreach (var entity in entities)
                entity.Id = new Guid();
            return base.CreateMany(entities, cancellationToken);
        }

        
    }


}
