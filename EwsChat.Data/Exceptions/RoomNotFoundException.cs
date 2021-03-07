using System;

namespace EwsChat.Data.Exceptions
{
    [Serializable]
    public class RoomNotFoundException : Exception
    {
        public RoomNotFoundException(string message) : base(message)
        {
        }
    }
}