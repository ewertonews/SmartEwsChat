using System.Net.Http;
using System.Threading.Tasks;

namespace EwsChat.Api.ExternalClients
{
    public class SomeClient : HttpClientWrapperBase<Something>, ISomeClient
    {
        //Client can be created here as a readonly static object or in the startup class with 
        //services.AddHttpClient<ISomeClient, SomeClient>(client =>
        //    {        
        //          client.BaseAddress = url here of from Configuration;
        //    });
        private readonly HttpClient _httpClient;

        public SomeClient(HttpClient httpClient) : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Something> GetSomethingAsync()
        {
            return await Get("https://someapi.ews.net");
        }

        public async Task<Something> PostSomethingAsync(Something aThing)
        {
            return await Post(aThing, "https://someapi.ews.net");
        }
    }
}
