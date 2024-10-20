namespace SaeedAzari.Core.Security.Identity.Exceptions
{
    public class UserNotFoundException : IdentityBaseException
    {
        public UserNotFoundException(string UserName) : base($"User {UserName} not found.")
        {
        }

        public UserNotFoundException(string UserName, Exception? innerException) : base($"User {UserName} not found.", innerException)
        {
        }
    }
}
