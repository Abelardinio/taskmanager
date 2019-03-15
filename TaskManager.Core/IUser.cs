using System;

namespace TaskManager.Core
{
    public interface IUser : IUserInfo, IUserLoginInfo
    {
        DateTime Created { get; }

        string PasswordSalt { get; }

        string Password { get; }
    }
}