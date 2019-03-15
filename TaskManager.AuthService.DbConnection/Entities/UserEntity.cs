using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core;

namespace TaskManager.AuthService.DbConnection.Entities
{
    [Table("Users")]
    public class UserEntity : IUser
    {
        public UserEntity() { }

        public UserEntity(IUserInfo user)
        {
            Username = user.Username;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Created = DateTime.Now;
            PasswordSalt = Guid.NewGuid().ToString();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}