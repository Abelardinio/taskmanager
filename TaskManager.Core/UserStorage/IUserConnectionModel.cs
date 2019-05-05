namespace TaskManager.Core.UserStorage
{
    public interface IUserConnectionModel
    {
        int UserId { get; }
        int[] ProjectIds { get; }
    }
}