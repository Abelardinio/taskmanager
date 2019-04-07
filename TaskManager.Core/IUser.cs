using System;

namespace TaskManager.Core
{
    public interface IUser 
    {
        int Id { get; }
        string Username { get; }
        DateTime Created { get; }
        string PasswordSalt { get; }
        string Password { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}