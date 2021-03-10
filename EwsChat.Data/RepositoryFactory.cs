using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private ChatContext _chatContext;
        private IChatRoomRepository _chatRoomRepository;
        private IChatUserRespository _chatUserRepository;
        private IMessageRepository _messageRepository;

        public RepositoryFactory(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public IChatRoomRepository ChatRoom
        {
            get
            {
                if (_chatRoomRepository == null)
                {
                    _chatRoomRepository = new ChatRoomRepository(_chatContext);
                }
                return _chatRoomRepository;
            }
        }

        public IChatUserRespository ChatUser
        {
            get
            {
                if (_chatUserRepository == null)
                {
                    _chatUserRepository = new ChatUserRespository(_chatContext);
                }
                return _chatUserRepository;
            }
        }

        public IMessageRepository Message
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new MessageRepository(_chatContext);
                }
                return _messageRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _chatContext.SaveChangesAsync();
        }
    }
}
