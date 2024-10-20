using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace SaeedAzari.Core.Security.Identity.Exceptions
{
    public class LoginException : IdentityBaseException
    {
        public LoginException(SignInResult result) : base(ModifyMessage(result))
        {
        }

        public LoginException(SignInResult result, Exception? innerException) : base(ModifyMessage(result), innerException)
        {

        }
        private static string ModifyMessage(SignInResult result)
        {
            if (result.IsLockedOut) return "user is Locked Out";
            if (!result.IsNotAllowed) return "user is Not Allowed";
            return "user is Not Succeeded";
        }
    }
}
