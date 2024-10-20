
using Microsoft.AspNetCore.Mvc;
using SaeedAzari.Core.Security.Identity.Entities;
using SaeedAzari.Core.Security.Identity.Models;

namespace SaeedAzari.Core.Security.Identity.Services
{
    public interface IAuthenticationService<TBaseIdentityUser, TBaseIdentityRole>
         where TBaseIdentityUser : BaseIdentityUser
            where TBaseIdentityRole : BaseIdentityRole
    {
        Task<string> AddRole(TBaseIdentityRole RoleName);
        Task AddToRole(string UserName, string RoleName);
        Task<TokenResult> Login(string username, string password);
        Task<TokenResult> Login(Login loginModel);
        Task<string> Register(string userName, string password, Dictionary<string, object> OtherClaims);
        Task<string> Register<T>(T registerModel) where T : Register;
        Task ResetPassword([FromBody] ResetPassword model);
    }
    public interface IAuthenticationService<TBaseIdentityUser> : IAuthenticationService<TBaseIdentityUser, BaseIdentityRole>
        where TBaseIdentityUser : BaseIdentityUser
    {
    }
    public interface IAuthenticationService : IAuthenticationService<BaseIdentityUser>
    {
    }

}
