using TaskManager.Core;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.DbConnection.Models
{
    public class UserLoginInfoModel : IUserLoginInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public Role Role { get; set; }
    }
}
