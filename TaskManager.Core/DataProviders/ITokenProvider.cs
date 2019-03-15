namespace TaskManager.Core.DataProviders
{
    public interface ITokenProvider
    {
        string Get(IUserLoginInfo info);
    }
}