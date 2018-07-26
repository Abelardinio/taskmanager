using System;

namespace TaskManager.Core.Exceptions
{
    public class InvalidArgumentException : TaskManagerException
    {
        public InvalidArgumentException(string message) : base(message)
        {
        }

        public InvalidArgumentException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}