using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.DbConnection.Entities
{
    [Table("UserPermissions")]
    public class UserPermissionEntity
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        [Required]
        public Permission Permission { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}