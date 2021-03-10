using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IRepositoryFactory
    {
        IChatRoomRepository ChatRoom { get; }
        IChatUserRespository ChatUser { get; }
        IMessageRepository Message { get; }

        Task SaveAsync();
    }
}