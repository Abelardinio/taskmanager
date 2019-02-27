using System;
using TaskManager.Core;

namespace TaskManager.AuthService.WebApi.Models
{
    public class UserModel : IUserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}