using TaskManager.Core.UserStorage;

namespace TaskManager.MessagingService.Data.Models
{
    public class UserConnectionModel : IUserConnectionModel
    {
        public int UserId { get; set; }
        public int[] ProjectIds { get; set; }
    }
}