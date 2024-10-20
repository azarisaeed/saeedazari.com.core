using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SaeedAzari.Core.Security.Identity.Entities;
using SaeedAzari.Core.Security.Identity.Exceptions;
using SaeedAzari.Core.Security.Identity.Models;

namespace SaeedAzari.Core.Security.Identity.Services
{
    public class AuthenticationService<TBaseIdentityUser, TBaseIdentityRole>(UserManager<TBaseIdentityUser> userManager,
                                                                             SignInManager<TBaseIdentityUser> signInManager,
                                                                             RoleManager<TBaseIdentityRole> roleManager,
                                                                             IConfiguration configuration) :
        IAuthenticationService<TBaseIdentityUser, TBaseIdentityRole>
        where TBaseIdentityUser : BaseIdentityUser
            where TBaseIdentityRole : BaseIdentityRole
    {
        private readonly UserManager<TBaseIdentityUser> userManager = userManager;
        private readonly SignInManager<TBaseIdentityUser> _signInManager = signInManager;
        private readonly RoleManager<TBaseIdentityRole> _RoleManager = roleManager;
        private readonly IConfiguration _configuration = configuration;
        public async Task<string> Register(string userName, string password, Dictionary<string, object> OtherClaims)
        {
            var userExists = await userManager.FindByNameAsync(userName);
            if (userExists != null) throw new UserFoundException("userName");

            TBaseIdentityUser user = Activator.CreateInstance<TBaseIdentityUser>();

            user.UserName = userName;
            user.LockoutEnabled = true;
            foreach (var clm in OtherClaims)
                try
                {
                    var property = typeof(TBaseIdentityUser).GetProperty(clm.Key);
                    property?.SetValue(user, clm.Value);

                }
                catch (Exception)
                {
                }

            var createUserResult = await userManager.CreateAsync(user, password);
            if (!createUserResult.Succeeded) throw new UserIdentityErrorException(createUserResult.Errors);
            return user.Id;
        }

        public async Task<string> AddRole(TBaseIdentityRole Role)
        {
            if (!await _RoleManager.RoleExistsAsync(Role.Name))
                await _RoleManager.CreateAsync(Role);
            return Role.Id;
        }
        public async Task AddToRole(string UserName, string RoleName)
        {
            if (await _RoleManager.RoleExistsAsync(RoleName))
            {
                var user = await userManager.FindByNameAsync(UserName);
                await userManager.AddToRoleAsync(user, RoleName);
            }
        }
        public async Task ResetPassword(ResetPassword model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username) ?? throw new UserNotFoundException(model.Username);
            await userManager.RemovePasswordAsync(userExists);

            var result = await userManager.AddPasswordAsync(userExists, model.Password);
            if (!result.Succeeded)
                throw new UserIdentityErrorException(result.Errors);
        }
        public Task<string> Register<T>(T registerModel) where T : Register
        {
            var OtherProperties = registerModel.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).ToList();
            Dictionary<string, object> OtherClaims = [];
            foreach (var property in OtherProperties)
                OtherClaims.Add(property.Name, property.GetValue(registerModel, null));
            return Register(registerModel.UserName, registerModel.Password, OtherClaims);
        }
        public async Task<TokenResult> Login(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username) ?? throw new UserFoundException(username);
            var signInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: true);
            if (!signInResult.Succeeded) throw new LoginException(signInResult);
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new(ClaimTypes.Name, user.UserName??""),
               new(nameof(user.Id), user.Id),
               new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var OtherProperties = user.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly).ToList();
            foreach (var property in OtherProperties)
                try
                {
                    authClaims.Add(new Claim(property.Name, property.GetValue(user)?.ToString() ?? ""));
                }
                catch (Exception)
                {
                }
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GenerateToken(authClaims);

            return token;
        }
        public Task<TokenResult> Login(Login loginModel)
        {
            return Login(loginModel.UserName, loginModel.Password);
        }

        private TokenResult GenerateToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResult() { Token = tokenHandler.WriteToken(token), ValidTo = token.ValidTo };
        }

    }
    public class AuthenticationService<TBaseIdentityUser>(UserManager<TBaseIdentityUser> userManager,
                                                          SignInManager<TBaseIdentityUser> signInManager,
                                                          RoleManager<BaseIdentityRole> roleManager,
                                                          IConfiguration configuration) :
        AuthenticationService<TBaseIdentityUser, BaseIdentityRole>(userManager, signInManager, roleManager, configuration),
        IAuthenticationService<TBaseIdentityUser>
        where TBaseIdentityUser : BaseIdentityUser
    {
    }
    public class AuthenticationService(UserManager<BaseIdentityUser> userManager,
                                                          SignInManager<BaseIdentityUser> signInManager,
                                                          RoleManager<BaseIdentityRole> roleManager,
                                                          IConfiguration configuration) :
        AuthenticationService<BaseIdentityUser>(userManager, signInManager, roleManager, configuration),
        IAuthenticationService
    {
    }
}
