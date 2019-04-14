using System.Collections.Generic;
using TaskManager.Core;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.WebApi.Models
{
    public class ProjectPermissionModel : IProjectPermission
    {
        public int ProjectId { get; set; }
        public Permission[] Permissions { get; set; }
    }
}