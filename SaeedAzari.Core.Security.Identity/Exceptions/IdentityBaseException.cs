using System.Runtime.Serialization;

namespace SaeedAzari.Core.Security.Identity.Exceptions
{
    public abstract class IdentityBaseException : Exception
    {
        protected IdentityBaseException()
        {
        }

        protected IdentityBaseException(string? message) : base(message)
        {
        }

        protected IdentityBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected IdentityBaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
