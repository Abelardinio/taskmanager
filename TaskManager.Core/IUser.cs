using System;

namespace TaskManager.Core
{
    public interface IUser : IUserInfo
    {
        int Id { get; }

        DateTime Created { get; }
    }
}