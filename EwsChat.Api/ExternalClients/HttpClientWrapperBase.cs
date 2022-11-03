using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EwsChat.Api.ExternalClients
{
    public abstract class HttpClientWrapperBase<T> : IHttpClientWrapperBase<T> where T : class
    {
        private readonly HttpClient _httpClient;

        protected HttpClientWrapperBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Get(string url, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

            CreateRequestHeaders(headers, request);

            return  await HandleRequest(request);
        }

        public async Task<T> Post(T postObject, string url, Dictionary<string, string> headers = null)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = new StringContent(
                JsonSerializer.Serialize(postObject, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }),
                Encoding.UTF8, "application/json");

            CreateRequestHeaders(headers, request);

            return await HandleRequest(request);
        }

        private static void CreateRequestHeaders(Dictionary<string, string> headers, HttpRequestMessage request)
        {
            if (headers == null) return;
            foreach (var headerKey in headers.Keys)
            {
                request.Headers.Add(headerKey, headers[headerKey]);
            }
        }

        private async Task<T> HandleRequest(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(responseContent);

            return result;
        }
    }
}
