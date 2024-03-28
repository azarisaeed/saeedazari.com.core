

namespace SaeedAzari.Core.Repositories.EF.Context
{
    public class Option<TSqlserverDbContext> where TSqlserverDbContext : CoreDBContext
    {
        internal string ConnectionString;

        internal Option(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
