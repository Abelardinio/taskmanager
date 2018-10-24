using System;

namespace TaskManager.Core.Exceptions
{
    public class NotFoundException : TaskManagerException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}