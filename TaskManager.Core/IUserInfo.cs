namespace TaskManager.Core
{
    public interface IUserInfo
    {
        string Username { get; }

        string Email { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}