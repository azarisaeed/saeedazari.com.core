using Microsoft.Data.SqlClient;

namespace SaeedAzari.Core.Repositories.EF.SPComponents.Models
{
    public interface ISPResultModel
    {
        Task FillWithDataReaderAsync(SqlDataReader reader);

    }
}
