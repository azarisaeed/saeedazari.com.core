using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using SaeedAzari.Core.Repositories.EF.SPComponents.Models;

namespace SaeedAzari.Core.Repositories.EF.Context
{
   

    public class CoreDBContext(Option<CoreDBContext> Options) : DbContext
    {
        private readonly string _connectionString = Options.ConnectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.GetInterfaces().Any(inter => inter.IsGenericType && inter.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in types)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);

        }
        public DataSet ExecuteStoredProceture(string Name, params object[] parameterValues)
        {
            var ds = new DataSet();
            using (var cnn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(Name, cnn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = int.MaxValue,
                };
                cmd.Parameters.Clear();
                SetParameters(parameterValues, cnn, cmd);
                GetResult(ds, cnn, cmd);
            }

            return ds;
        }
        public Task<List<T>> ExecuteStoredProcedure<T>(string Name, params object[] parameterValues) where T : class
        {
            return Database.SqlQueryRaw<T>(Name, parameterValues).ToListAsync();
        }
        public Task<int> ExecuteScalarStoredProcedure<T>(string Name, params object[] parameterValues) where T : class
        {
            return Database.ExecuteSqlRawAsync(Name, parameterValues);
        }
        public DataSet ExecuteStoredProceture(string Name, Dictionary<string, object> parameterValues)
        {

            var ds = new DataSet();
            using (var cnn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(Name, cnn)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = int.MaxValue,
                };
                cmd.Parameters.Clear();

                foreach (var item in parameterValues)
                    cmd.Parameters.Add(new SqlParameter(item.Key, item.Value ?? DBNull.Value));

                GetResult(ds, cnn, cmd);
            }

            return ds;
        }

        private void GetResult(DataSet ds, SqlConnection cnn, SqlCommand cmd)
        {
            cnn.Open();
            var adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            cnn.Close();
        }

        private void SetParameters(object[] parameterValues, SqlConnection cnn, SqlCommand cmd)
        {

            cnn.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            cnn.Close();

            cmd.Parameters.RemoveAt(0);

            var DiscoverSpParameterSet = new SqlParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(DiscoverSpParameterSet, 0);

            AssignParameterValues(DiscoverSpParameterSet, parameterValues);
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(DiscoverSpParameterSet);

        }
        private void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if (commandParameters is null && parameterValues is null)
                return;

            if (commandParameters.Length != parameterValues.Length)
                throw new ArgumentException("Parameter count does not match Parameter Value count.");

            int j = commandParameters.Length - 1;
            var loopTo = j;

            for (int i = 0; i <= loopTo; i++)
                commandParameters[i].Value = parameterValues[i] is IDbDataParameter parameter
                    ? parameter.Value is null ? DBNull.Value : parameter.Value
                    : parameterValues[i] is null ? DBNull.Value : parameterValues[i];

        }


        public async Task<List<T>> ExecuteSpAsync<T>(string spName, IFilterModel FilterModel, CancellationToken cancellationToken = default) where T : ISPResultModel, new()
        {
            List<T> result = [];
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync(cancellationToken);
                using (var comm = new SqlCommand(spName, conn))
                {
                    comm.CommandTimeout = int.MaxValue; comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddRange(FilterModel.GetParameters());
                    SqlDataReader reader = await comm.ExecuteReaderAsync(cancellationToken);
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync(cancellationToken))
                        {
                            var res = new T();
                            await res.FillWithDataReaderAsync(reader);
                            result.Add(res);
                        }

                    }
                }
                conn.Close();
            }
            return result;
        }
    }
}
