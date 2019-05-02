using System;
using TaskManager.Core;
using TaskManager.Core.Enums;

namespace TaskManager.Common.AspNetCore.Model
{
    [Serializable]
    public class ProjectPermissionModel : IProjectPermission
    {
        public int ProjectId { get; set; }
        public Permission[] Permissions { get; set; }
    }
}