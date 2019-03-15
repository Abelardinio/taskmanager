namespace TaskManager.Core
{
    public interface IUserLoginInfo : IUsername
    {
        int Id { get; }
    }
}