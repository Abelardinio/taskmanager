using System;
using TaskManager.Core;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.WebApi.Models
{
    public class UserInfoModel : IUserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}