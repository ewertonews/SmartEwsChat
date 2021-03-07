using System;

namespace EwsChat.Data.Exceptions
{
    [Serializable]
    public class InvalidMessageException : Exception
    {
        public InvalidMessageException(string message) : base(message)
        {
        }
    }
}