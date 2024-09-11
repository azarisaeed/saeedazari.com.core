
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
        where TEntity : IEntity<TKey>
       where TContext : CoreDBContext
    {
        protected readonly IQueryable<TEntity> Collection = (IQueryable<TEntity>)((IDbSetCache)Db).GetOrAddSet(Db.GetDependencies().SetSource, typeof(TEntity));
        protected readonly DatabaseFacade SqlServerDatabase = Db.Database;
        protected readonly TContext Db = Db;
        protected readonly IApplicationContext ApplicationContext = applicationContext;

        public virtual async Task Delete(TKey id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await GetById(id, cancellationToken);
            await Delete(entity, cancellationToken);
        }
        public virtual async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Db.Remove(entity);
            await Db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteMany(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entities = await GetByIds(ids, cancellationToken);
            await DeleteMany(entities, cancellationToken);
        }

        public virtual async Task DeleteMany(IEnumerable<TEntity> Entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Db.RemoveRange((IEnumerable<object>)Entities);
            await Db.SaveChangesAsync(cancellationToken);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await Collection.ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task Create(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Db.Add(entity);
            await Db.SaveChangesAsync(cancellationToken);

        }


        public virtual async Task CreateMany(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Db.AddRangeAsync((IEnumerable<object>)entities, cancellationToken);
            await Db.SaveChangesAsync(cancellationToken);
        }


        public virtual async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Db.Update(entity);
            await Db.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = Collection.Where(filter);
            return await findResult.ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var findResult = Collection.Where(filter);
            return await findResult.AnyAsync(cancellationToken: cancellationToken);
        }
        public virtual async Task<TEntity?> GetById(TKey id, CancellationToken cancellationToken = default)
        {
            IEnumerable<TEntity> Items = await Find(s => s.Id.Equals(id), cancellationToken);
            TEntity? Item = Items.FirstOrDefault();
            //Db.Entry(Item).State = EntityState.Detached;
            return Item;
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIds(IEnumerable<TKey> ids, CancellationToken cancellationToken = default)
        {
            return await Find(s => ids.Contains(s.Id), cancellationToken);
        }
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

        public IQueryable<TEntity> AsQueryable()
        {
            return Collection;
        }


    }
   

    public class EntityRepository<TEntity, TContext>(TContext SqlServerDbContext, IApplicationContext applicationContext) : EntityRepository<Guid, TEntity, TContext>(SqlServerDbContext, applicationContext), IEntityRepository<TEntity>
       where TEntity : IEntity
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
