using TaskManager.Core.Enums;

namespace TaskManager.Core
{
    public interface ITokenInfo
    {
        int Id { get; }
        string Username { get; }
        Role Role { get; }
    }
}