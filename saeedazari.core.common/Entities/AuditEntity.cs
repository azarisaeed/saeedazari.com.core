
namespace SaeedAzari.core.entities
{
    public interface IAbstractAuditEntity<TKey> : IAbstractEntity<TKey> where TKey : IEquatable<TKey>
    {
        string CreatedBy { get; set; } // UserName
        DateTime CreatedDate { get; set; }
        string LastUpdatedBy { get; set; } // UserName
        DateTime LastUpdatedDate { get; set; }
    }
    public interface IAuditEntity<TKey> : IAbstractAuditEntity<TKey>, IEntity<TKey> where TKey : IEquatable<TKey>
    {
    }

    public interface IAuditEntity : IAuditEntity<Guid>, IEntity
    {
    }
    public class AbstractAuditEntity<TKey> : IAbstractAuditEntity<TKey> where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; } // UserName
        public DateTime CreatedDate { get; set; }

        public string LastUpdatedBy { get; set; } // UserName
        public DateTime LastUpdatedDate { get; set; }
    }
    public class AuditEntity<TKey> : AbstractAuditEntity<TKey>, IAuditEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public class AuditEntity : AuditEntity<Guid>, IAuditEntity
    {
    }


}