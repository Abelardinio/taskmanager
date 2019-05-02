using TaskManager.Common.AspNetCore.Model;

namespace TaskManager.AuthService.WebApi.Models
{
    public class PermissionsModel
    {
        public ProjectPermissionModel[] Permissions { get; set; }
    }
}