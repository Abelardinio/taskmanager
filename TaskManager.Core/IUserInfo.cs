namespace TaskManager.Core
{
    public interface IUserInfo : IUsername
    {

        string Email { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}