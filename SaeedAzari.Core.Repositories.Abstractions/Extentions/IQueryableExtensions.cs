using SaeedAzari.core.Common;
using SaeedAzari.core.entities;
using System.Linq.Expressions;
using System.Reflection;

namespace SaeedAzari.Core.Repositories.Abstractions.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, string propertyName, OrderbyTypes orderbyType)
        {
            var entityType = typeof(TSource);
            var ordebyTypesname = orderbyType.ToString();
            //Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName);
            ParameterExpression arg = Expression.Parameter(entityType, "x");
            MemberExpression property = Expression.Property(arg, propertyName);
            var selector = Expression.Lambda(property, [arg]);
            //Get System.Linq.Queryable.OrderBy() method.
            var enumarableType = typeof(Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == ordebyTypesname && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     //Put more restriction here to ensure selecting the right overload                
                     return parameters.Count == 2;//overload that has 2 parameters
                 }).Single();
            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            if (propertyInfo is null)
                return query;
            MethodInfo genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it.*/

            try
            {
                var newQuery = (IOrderedQueryable<TSource>)genericMethod
                .Invoke(genericMethod, [query, selector]);
                query = newQuery.AsQueryable();
            }
            catch (Exception)
            {

            }

            return query;
        }
        public static TEntity SetPropertiesOnCreate<TKey, TEntity>(this TEntity entity, IApplicationContext appContext)
                     where TEntity : IAuditEntity<TKey>
                        where TKey : IEquatable<TKey>
        {
            var now = DateTime.Now;
            var userName = appContext.UserName;

            entity.CreatedDate = now;
            entity.CreatedBy = userName;
            entity.LastUpdatedDate = now;
            entity.LastUpdatedBy = userName;
            return entity;
        }

        public static TEntity SetPropertiesOnUpdate<TKey, TEntity>(this TEntity entity, IApplicationContext appContext)
            where TEntity : IAuditEntity<TKey>
                        where TKey : IEquatable<TKey>
        {
            var now = DateTime.Now;
            var userName = appContext.UserName;

            entity.LastUpdatedDate = now;
            entity.LastUpdatedBy = userName;
            return entity;
        }
        public static IEnumerable<TEntity> SetPropertiesOnCreate<TKey, TEntity>(this IEnumerable<TEntity> entities, IApplicationContext appContext) where TKey : IEquatable<TKey>
        where TEntity : IAuditEntity<TKey>
        {
            var now = DateTime.Now;
            var userName = appContext.UserName;
            foreach (var entity in entities)
            {
                entity.CreatedDate = now;
                entity.CreatedBy = userName;
                entity.LastUpdatedDate = now;
                entity.LastUpdatedBy = userName;
            }
            return entities;
        }

        public static IEnumerable<TEntity> SetPropertiesOnUpdate<TKey, TEntity>(this IEnumerable<TEntity> entities, IApplicationContext appContext) where TKey : IEquatable<TKey>
       where TEntity : IAuditEntity<TKey>
        {
            var now = DateTime.Now;
            var userName = appContext.UserName;
            foreach (var entity in entities)
            {
                entity.LastUpdatedDate = now;
                entity.LastUpdatedBy = userName;
            }
            return entities;
        }




    }
}
