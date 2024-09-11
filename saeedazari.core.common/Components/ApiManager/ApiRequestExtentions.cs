using Newtonsoft.Json;
using System.Data;

namespace saeedazari.core.common.Components.ApiManager
{
    public static class ApiRequestExtentions
    {
        public static string Serialize(this object data)
        {
            if (data.GetType() == typeof(DataTable))
                return JsonConvert.SerializeObject(((DataTable)data).DataTableToDataModel());
            else if (data.GetType() == typeof(DataSet))
                return JsonConvert.SerializeObject(((DataSet)data).Tables[0].DataTableToDataModel());
            else
                return JsonConvert.SerializeObject(data);
        }
        public static List<Dictionary<string, object>> DataTableToDataModel(this DataTable data)
        {
            var result = new List<Dictionary<string, object>>();
            foreach (DataRow r in data.Rows)
            {
                var dic = new Dictionary<string, object>();
                foreach (DataColumn c in data.Columns)
                    dic.Add(c.ColumnName, r[c]);
                result.Add(dic);
            }
            return result;
        }
        public static T Deserialize<T>(this string jsonString) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
