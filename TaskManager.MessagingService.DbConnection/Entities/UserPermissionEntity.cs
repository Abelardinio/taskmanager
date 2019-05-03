using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Common.DbConnection.Entities;
using TaskManager.Core.Enums;

namespace TaskManager.MessagingService.DbConnection.Entities
{
    [Table("UserPermissions")]
    public class UserPermissionEntity : IUserPermissionEntity
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public Permission Permission { get; set; }
        [Required]
        public int ProjectId { get; set; }
    }
}