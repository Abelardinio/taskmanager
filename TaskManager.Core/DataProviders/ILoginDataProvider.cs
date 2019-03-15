using System.Threading.Tasks;

namespace TaskManager.Core.DataProviders
{
    public interface ILoginDataProvider
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}