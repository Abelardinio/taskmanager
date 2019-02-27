﻿namespace TaskManager.Core
{
    public interface IUser
    {
        int Id { get; }

        string Username { get; }

        string Email { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}