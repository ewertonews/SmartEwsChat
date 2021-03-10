using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Api.ExternalClients
{
    public interface IHttpClientWrapperBase<T> where T : class
    {
        Task<T> Get(string url, Dictionary<string, string> headers = null);
        Task<T> Post(T postObject, string url, Dictionary<string, string> headers = null);
    }
}