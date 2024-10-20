
using Microsoft.AspNetCore.Identity;

namespace SaeedAzari.Core.Security.Identity.Exceptions
{
    internal class UserIdentityErrorException : IdentityBaseException
    {
      public  IEnumerable<IdentityError> Errors {  get; }
        public UserIdentityErrorException(IEnumerable<IdentityError> Errors) : base(string.Join(";",Errors.Select(i=>$"[{i.Code}]{i.Description}")))
        {
            this.Errors = Errors;
        }

        public UserIdentityErrorException(IEnumerable<IdentityError> Errors, Exception? innerException) : base(string.Join(";", Errors.Select(i => $"[{i.Code}]{i.Description}")), innerException)
        {
            this.Errors = Errors;

        }
    }
}
