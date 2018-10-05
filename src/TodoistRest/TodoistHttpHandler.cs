using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TodoistRest
{
    public interface ITodoistHttpHandler
    {
        Task<T> GetAsync<T>(string resource);
    }

    class TodoistHttpHandler : ITodoistHttpHandler
    {
        private static readonly Uri BaseUri = new Uri("https://beta.todoist.com/API/v8");
        private static readonly HttpClient Client = new HttpClient();

        private readonly string _apiToken;
        private readonly AuthenticationHeaderValue _authHeader;

        public TodoistHttpHandler(string apiToken)
        {
            _apiToken = apiToken;
            _authHeader = new AuthenticationHeaderValue("Bearer", _apiToken);
        }

        public async Task<T> GetAsync<T>(string resource)
        {
            var requestId = Guid.NewGuid().ToString();
            var message = new HttpRequestMessage(HttpMethod.Get, new Uri(BaseUri, resource));
            message.Headers.Authorization = _authHeader;
            message.Headers.Add("X-Request-Id", requestId);

            var response = await Client.SendAsync(message);
            EnsureValidResponse(response);
            
            return await response.Content.ReadAsAsync<T>();
        }

        private static void EnsureValidResponse(HttpResponseMessage response)
        {
            // TODO: Add better handling here
            if(!response.IsSuccessStatusCode)
                throw new TodoistRestException($"Received an unsuccessful status code: {response.StatusCode}");
        }
    }

    public class TodoistRestException : Exception
    {
        public TodoistRestException() { }
        public TodoistRestException(string message) : base(message) { }
        public TodoistRestException(string message, System.Exception inner) : base(message, inner) { }
    }
}
