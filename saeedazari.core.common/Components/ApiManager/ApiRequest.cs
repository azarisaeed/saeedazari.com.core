using System.Net;
using System.Text;

namespace saeedazari.core.common.Components.ApiManager
{
    public class ApiRequest
    {
        private Dictionary<string, string> Headers;

        public ApiRequest()
        {
            Headers = new Dictionary<string, string>();
        }

        public ApiRequest(Dictionary<string, string> headers)
        {
            Headers = headers;
        }
        public void AddHeader(string Key, string Value)
        {
            Headers.Add(Key, Value);
        }
        public WebResponse Send(string url, object obj)
        {
            var req = WebRequest.Create(url);
            req.ContentType = "application/json";
            req.Method = "POST";
            foreach (var Header in Headers)
                req.Headers[Header.Key] = Header.Value;
            string json;
            if (obj.GetType() == typeof(string))
                json = (string)obj;
            else
                json = obj.Serialize();
            var data = Encoding.UTF8.GetBytes(json);
            req.ContentLength = data.Length;
            using var requestStream = req.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            return req.GetResponse();

        }

        public string ReadResponse(WebResponse Respone)
        {
            var reader = new StreamReader(Respone.GetResponseStream());
            var result = reader.ReadToEnd();
            reader.Dispose();
            return result;

        }
    }
}
