using System;
using TaskManager.Core;

namespace TaskManager.AuthService.WebApi.Models
{
    public class UserModel : UserInfoModel
    {
        public int Id { get; set; }
        public DateTime Created { get; }

        public UserModel() { }

        public UserModel(IUser user)
        {
            Id = user.Id;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Created = user.Created;
        }
    }
}