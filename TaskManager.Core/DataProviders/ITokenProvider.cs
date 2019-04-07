namespace TaskManager.Core.DataProviders
{
    public interface ITokenProvider
    {
        string Get(ITokenInfo info);
    }
}