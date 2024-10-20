using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedAzari.Core.Security.Identity.Exceptions
{
    public class UserFoundException : IdentityBaseException
    {
        public UserFoundException(string UserName) : base($"User {UserName} not found.")
        {
        }

        public UserFoundException(string UserName, Exception? innerException) : base($"User {UserName} not found.", innerException)
        {
        }
    }
}
