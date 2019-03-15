namespace TaskManager.Core
{
    public interface IAuthenticationSettings
    {
        string SecretKey { get; }
    }
}