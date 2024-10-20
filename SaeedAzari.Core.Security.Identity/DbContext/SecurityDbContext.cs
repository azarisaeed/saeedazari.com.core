using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaeedAzari.Core.Security.Identity.Entities;

namespace SaeedAzari.Core.Security.Identity.DbContext
{
    public class SecurityDbContext<TBaseIdentityUser, TBaseIdentityRole>(DbContextOptions options) : IdentityDbContext<TBaseIdentityUser, TBaseIdentityRole, string>(options)
       where TBaseIdentityUser : BaseIdentityUser
       where TBaseIdentityRole : BaseIdentityRole
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("IAAA");
            base.OnModelCreating(builder);
        }
    }
    public class SecurityDbContext<TBaseIdentityUser>(DbContextOptions options) : SecurityDbContext<TBaseIdentityUser, BaseIdentityRole>(options)
      where TBaseIdentityUser : BaseIdentityUser
    {
        
    }
    public class SecurityDbContext(DbContextOptions options) : SecurityDbContext<BaseIdentityUser, BaseIdentityRole>(options)
    {

    }
}
