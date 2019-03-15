using System;

namespace TaskManager.Core.Exceptions
{
    public class AuthException : TaskManagerException
    {
        public AuthException(string message) : base(message)
        {
        }

        public AuthException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}