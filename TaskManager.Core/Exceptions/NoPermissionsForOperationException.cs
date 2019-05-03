using System;

namespace TaskManager.Core.Exceptions
{
    public class NoPermissionsForOperationException : TaskManagerException
    {
        public NoPermissionsForOperationException(string message) : base(message)
        {
        }

        public NoPermissionsForOperationException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}