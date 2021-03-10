using System.Threading.Tasks;

namespace EwsChat.Api.ExternalClients
{
    public interface ISomeClient : IHttpClientWrapperBase<Something>
    {
        Task<Something> PostSomethingAsync(Something aThing);

        Task<Something> GetSomethingAsync();
    }
}
