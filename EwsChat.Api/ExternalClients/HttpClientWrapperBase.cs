using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EwsChat.Api.ExternalClients
{
    public abstract class HttpClientWrapperBase<T> : IHttpClientWrapperBase<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public HttpClientWrapperBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get(string url, Dictionary<string, string> headers = null)
        {
            T result = null;

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

            CreateRequestHeaders(headers, request);

            result = await HandleRequest(result, request);

            return result;
        }

        public async Task<T> Post(T postObject, string url, Dictionary<string, string> headers = null)
        {
            T result = null;

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = new StringContent(
                JsonSerializer.Serialize(postObject, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }),
                Encoding.UTF8, "application/json");

            CreateRequestHeaders(headers, request);

            result = await HandleRequest(result, request);

            return result;
        }

        private static void CreateRequestHeaders(Dictionary<string, string> headers, HttpRequestMessage request)
        {
            if (headers != null)
            {
                foreach (var headerKey in headers.Keys)
                {
                    request.Headers.Add(headerKey, headers[headerKey]);
                }
            }
        }

        private async Task<T> HandleRequest(T result, HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<T>(responseContent);

            return result;
        }
    }
}
