namespace SaeedAzari.core.entities
{
    public interface IAbstractEntity<TKey> where TKey : IEquatable<TKey>
    {
    }


    public interface IEntity<TKey> : IAbstractEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntity : IEntity<Guid>
    {
    }

    public class AbstractEntity<TKey> : IAbstractEntity<TKey> where TKey : IEquatable<TKey>
    {
    }
    public class Entity<TKey> : AbstractEntity<TKey>, IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
    }

    public class Entity : Entity<Guid>, IEntity
    {
    }
}
