namespace TaskManager.Core
{
    public interface IUserLoginInfo : ITokenInfo
    {
        string Password { get; }
        string PasswordSalt { get; }
    }
}