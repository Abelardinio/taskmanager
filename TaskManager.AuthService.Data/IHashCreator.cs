namespace TaskManager.AuthService.Data
{
    public interface IHashCreator
    {
        string Create(string username, string password, string passwordSalt);
    }
}