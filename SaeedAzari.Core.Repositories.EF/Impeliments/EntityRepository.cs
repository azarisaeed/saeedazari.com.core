
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;
using SaeedAzari.core.Common;
using SaeedAzari.core.entities;
using SaeedAzari.Core.Repositories.EF.Context;
using SaeedAzari.Core.Repositories.Abstractions.Interfaces;
using SaeedAzari.core.entities.models;
using SaeedAzari.Core.Repositories.Abstractions.Extensions;


namespace SaeedAzari.Core.Repositories.EF
{
    public class EntityRepository<TKey, TEntity, TContext>(TContext Db, IApplicationContext applicationContext) : IEntityRepository<TKey, TEntity>
          where TKey : IEquatable<TKey>
          where TEntity : class, IEntity<TKey>
         where TContext : CoreDBContext
    {
        protected DbSet<TEntity> Collection => Db.Set<TEntity>();
        protected TContext DataBase => Db;
        public IApplicationContext ApplicationContext => applicationContext;
        public virtual Task Delete(TKey id, CancellationToken cancellationToken = default) =>
            Collection.Where(i => i.Id.Equals(id)).ExecuteDeleteAsync(cancellationToken);

        public virtual Task Delete(TEntity entity, CancellationToken cancellationToken = default) =>
            Collection.Where(i => i.Id.Equals(entity.Id)).ExecuteDeleteAsync(cancellationToken);

        public virtual Task DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default) =>
            Collection.Where(i => ids.Contains(i.Id)).ExecuteDeleteAsync(cancellationToken);

        public virtual Task DeleteMany(IEnumerable<TEntity> Entities, CancellationToken cancellationToken = default) =>
            Collection.Where(i => Entities.Select(i => i.Id).Contains(i.Id)).ExecuteDeleteAsync(cancellationToken);

        public virtual Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default) =>
            Collection.ToListAsync(cancellationToken: cancellationToken);

        public virtual Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            Db.Add(entity);
            return Db.SaveChangesAsync(cancellationToken);
        }

        public virtual Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Db.AddRange(entities);
            return Db.SaveChangesAsync(cancellationToken);
        }


        public virtual Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            Db.Update(entity);
            return Db.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default) =>
            Collection.Where(filter).ToListAsync(cancellationToken: cancellationToken);

        public virtual Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default) =>
            Collection.Where(filter).AnyAsync(cancellationToken: cancellationToken);
        public virtual Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default) =>
            Collection.Where(s => s.Id.Equals(id)).FirstOrDefaultAsync(cancellationToken);

        public virtual Task<List<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default) =>
             Collection.Where(s => ids.Contains(s.Id)).ToListAsync(cancellationToken);
        public virtual async Task<IListResult<TEntity>> Find(IBaseSearchModel<TKey, TEntity> SearchModel, CancellationToken cancellationToken = default)
        {

            var filter = SearchModel.Expression();
            IQueryable<TEntity> query;
            if (filter == null)
                query = Collection;
            else
                query = Collection.Where(filter);
            long totalCount = await query.LongCountAsync(cancellationToken: cancellationToken);

            if (SearchModel.RecordCount <= 0)
                SearchModel.RecordCount = 20;

            if (SearchModel.PageNumber <= 0)
                SearchModel.PageNumber = 1;

            if (SearchModel.Sorting != null && SearchModel.Sorting.Count > 0)
            {
                foreach (var sortItem in SearchModel.Sorting)
                {
                    if (sortItem.Value == true)
                        query = query.OrderBy(sortItem.Key, OrderbyTypes.OrderBy);
                    else
                        query = query.OrderBy(sortItem.Key, OrderbyTypes.OrderByDescending);
                }
            }


            var items = await query.Skip((SearchModel.PageNumber - 1) * SearchModel.RecordCount).Take(SearchModel.RecordCount).ToListAsync(cancellationToken: cancellationToken);

            return new ListResult<TEntity>(items, totalCount, SearchModel.PageNumber, SearchModel.RecordCount);
        }

        public IQueryable<TEntity> AsQueryable() => Collection;
    }


    public class EntityRepository<TEntity, TContext>(TContext SqlServerDbContext, IApplicationContext applicationContext) : EntityRepository<Guid, TEntity, TContext>(SqlServerDbContext, applicationContext), IEntityRepository<TEntity>
       where TEntity : class, IEntity
        where TContext : CoreDBContext
    {
        public override Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Id = Guid.NewGuid();
            return base.Create(entity, cancellationToken);
        }

        public override Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
                entity.Id = Guid.NewGuid();
            return base.CreateMany(entities, cancellationToken);
        }

    }

}
