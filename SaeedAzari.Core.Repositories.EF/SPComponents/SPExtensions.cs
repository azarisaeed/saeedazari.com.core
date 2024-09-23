using Microsoft.Data.SqlClient;

namespace SaeedAzari.Core.Repositories.EF.SPComponents
{
    public static class SPExtensions
    {
        public static T GetFieldValue<T>(this SqlDataReader reader, string ColumName)
        {
            return reader.GetFieldValue<T>(reader.GetOrdinal(ColumName));
        }
        public static Task<T> GetFieldValueAsync<T>(this SqlDataReader reader, string ColumName)
        {
            return reader.GetFieldValueAsync<T>(reader.GetOrdinal(ColumName));
        }
    }
}
