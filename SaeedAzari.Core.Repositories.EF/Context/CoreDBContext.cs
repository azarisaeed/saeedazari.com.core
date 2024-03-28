using Microsoft.EntityFrameworkCore;

namespace SaeedAzari.Core.Repositories.EF.Context
{
   

    public class CoreDBContext(Option<CoreDBContext> Options) : DbContext
    {
        private readonly string _connectionString = Options.ConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

     
    }
}
