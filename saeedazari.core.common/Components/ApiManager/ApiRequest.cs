using System.Net.Http.Headers;
using System.Text;

namespace saeedazari.core.common.Components.ApiManager
{
    public class ApiClient(string apiUrl) : HttpClient
    {
        public void AuthorizeBearer(string Token) => DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        public async Task<string?> PostAsync<T>(string Action, T Value, CancellationToken cancellationToken = default) where T : class
        {
            HttpResponseMessage response = await PostAsync(apiUrl + Action, content: GetStringContent(Value), cancellationToken: cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStringAsync(cancellationToken);
            return null;
        }
        public async Task<TResponse?> PostAsync<TValue, TResponse>(string Action, TValue Value, CancellationToken cancellationToken = default) where TValue : class where TResponse : class
        {
            string? response = await PostAsync(Action, Value, cancellationToken);
            if (response is null)
                return null;
            return response.Deserialize<TResponse>();
        }
        private StringContent GetStringContent<T>(T Value) where T : class => new(Value.Serialize(), Encoding.UTF8, "application/json");
    }
}
