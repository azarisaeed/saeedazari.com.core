using System.Linq.Expressions;

namespace SaeedAzari.core.entities.models;

public interface IBaseSearchModel<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : IAbstractEntity<TKey>
{
    public int PageNumber { get; set; }
    public int RecordCount { get; set; }
    public Dictionary<string, bool> Sorting { get; set; }
    public Expression<Func<TEntity, bool>> Expression();
}
public interface IBaseSearchModel<TEntity> : IBaseSearchModel<Guid, TEntity>
    where TEntity : IEntity<Guid>
{

}
public interface ISearchModel<TKey, TEntity, TModel> : IBaseSearchModel<TKey, TEntity>
    where TKey : IEquatable<TKey>
    where TEntity : IAbstractEntity<TKey>
{
    public TModel Model { get; set; }
    public Expression<Func<TEntity, bool>> Filter();
}
public interface ISearchModel<TEntity, TModel> : IBaseSearchModel<TEntity> where TEntity : IEntity
{

}

public abstract class BaseSearchModel<TKey, TEntity, TModel> : ISearchModel<TKey, TEntity, TModel>
    where TKey : IEquatable<TKey>
    where TEntity : IAbstractEntity<TKey>
{
    public int PageNumber { get; set; }
    public int RecordCount { get; set; }
    public Dictionary<string, bool> Sorting { get; set; }
    public TModel Model { get; set; }

    public BaseSearchModel()
    {
        PageNumber = 1;
        RecordCount = 10;
    }
    public Expression<Func<TEntity, bool>> Expression()
    {
        return Filter();
    }

    public abstract Expression<Func<TEntity, bool>> Filter();

}
public abstract class BaseSearchModel<TEntity, TModel> : BaseSearchModel<Guid, TEntity, TModel>
   where TEntity : IEntity
{
}

