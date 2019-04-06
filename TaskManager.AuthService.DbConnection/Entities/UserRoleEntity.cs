using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.DbConnection.Entities
{
    [Table("UserRoles")]
    public class UserRoleEntity
    {
        public int Id { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}