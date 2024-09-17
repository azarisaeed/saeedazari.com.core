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
        public HttpResponseMessage Send<T>(string url, T obj) where T : class
        {
            return SendAsync(url, obj).Result;
        }
        public async Task<HttpResponseMessage> SendAsync<T>(string url, T obj, CancellationToken cancellationToken = default) where T : class
        {
            var baseAddress = new Uri(url);
            var client = new HttpClient() { BaseAddress = baseAddress };
            var content = new StringContent(obj.Serialize(), Encoding.UTF8, "application/json");
            foreach (var Header in Headers)
                content.Headers.Add(Header.Key, Header.Value);
            HttpResponseMessage result = await client.PostAsync("", content, cancellationToken);
            return result;
        }

        public string ReadResponse(HttpResponseMessage Respone)
        {
            var reader = new StreamReader(Respone.Content.ReadAsStream());
            var result = reader.ReadToEnd();
            reader.Dispose();
            return result;

        }
        public T ReadResponse<T>(HttpResponseMessage Respone) where T : class
        {
            var reader = new StreamReader(Respone.Content.ReadAsStream());
            var result = reader.ReadToEnd();
            reader.Dispose();
            return result.Deserialize<T>();

        }
        public async Task<T> ReadResponseAsync<T>(HttpResponseMessage Respone, CancellationToken cancellationToken = default) where T : class
        {
            string jsonresult = await Respone.Content.ReadAsStringAsync(cancellationToken);
            return jsonresult.Deserialize<T>();
        }
        public async Task<string> ReadResponseAsync(HttpResponseMessage Respone, CancellationToken cancellationToken = default)
        {
            return await Respone.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}
