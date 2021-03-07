using System;

namespace EwsChat.Data.Exceptions
{
    [Serializable]
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message) : base(message)
        {
        }
    }
}