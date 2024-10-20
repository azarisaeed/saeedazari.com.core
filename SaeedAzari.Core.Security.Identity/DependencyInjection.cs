using Microsoft.Extensions.Configuration;
using SaeedAzari.Core.Security.Identity.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SaeedAzari.Core.Security.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SaeedAzari.Core.Security.Identity.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurity<TBaseIdentityUser, TBaseIdentityRole, DbContext>(this IServiceCollection services, IConfiguration Configuration, string connectionString)
            where TBaseIdentityUser : BaseIdentityUser
            where TBaseIdentityRole : BaseIdentityRole
            where DbContext : SecurityDbContext<TBaseIdentityUser, TBaseIdentityRole>
        {
            services.AddDbContext<DbContext>(x => x.UseSqlServer(Configuration.GetConnectionString(connectionString)));
            services.AddIdentity<TBaseIdentityUser, TBaseIdentityRole>(c => c.Lockout = new LockoutOptions() { MaxFailedAccessAttempts = 5, })
               .AddEntityFrameworkStores<DbContext>()
               .AddDefaultTokenProviders();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;


            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Issuer"],
                    ValidIssuer = Configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"])),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddScoped<IAuthenticationService<TBaseIdentityUser, TBaseIdentityRole>, AuthenticationService<TBaseIdentityUser, TBaseIdentityRole>>();
            services.AddAuthorization();


            return services;
        }
        public static IServiceCollection AddSecurity<TBaseIdentityUser, DbContext>(this IServiceCollection services, IConfiguration Configuration, string connectionString)
          where TBaseIdentityUser : BaseIdentityUser
          where DbContext : SecurityDbContext<TBaseIdentityUser>

        {
            services.AddScoped<IAuthenticationService<TBaseIdentityUser>, AuthenticationService<TBaseIdentityUser>>();

            return services.AddSecurity<TBaseIdentityUser, BaseIdentityRole, DbContext>(Configuration, connectionString);
        }
        public static IServiceCollection AddSecurity<DbContext>(this IServiceCollection services, IConfiguration Configuration, string connectionString)
         where DbContext : SecurityDbContext

        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services.AddSecurity<BaseIdentityUser, BaseIdentityRole, DbContext>(Configuration, connectionString);
        }
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration Configuration, string connectionString)

        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services.AddSecurity< SecurityDbContext>(Configuration, connectionString);
        }
    }
}
