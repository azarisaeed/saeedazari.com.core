using Microsoft.Data.SqlClient;

namespace SaeedAzari.Core.Repositories.EF.SPComponents.Models
{
    public interface IFilterModel
    {
        /// <summary>
        ///  PropertyName = await reader.GetFieldValueAsync<int?>(nameof(PropertyName));
        /// </summary>
        /// <returns></returns>
        SqlParameter[] GetParameters();
    }
}
