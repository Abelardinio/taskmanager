using System;

namespace TaskManager.MessagingService
{
    public interface IMessagingService : IDisposable
    {
        void Start();
    }
}