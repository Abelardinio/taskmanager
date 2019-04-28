using System.Collections.Generic;
using TaskManager.Common.AspNetCore.Model;
using TaskManager.Core;
using TaskManager.Core.Enums;

namespace TaskManager.AuthService.WebApi.Models
{
    public class Permissions
    {
        public const string Admin = "Administrator";
        public const string CreateFeature = "Create Feature";
        public const string CreateTask = "Create Task";
        public const string Read = "Read";

        public static ILookup[] Lookup =
        {
            new LookupModel {Id = (int) Permission.Admin, Name = Admin},
            new LookupModel {Id = (int) Permission.CreateFeature, Name = CreateFeature},
            new LookupModel {Id = (int) Permission.CreateTask, Name = CreateTask},
            new LookupModel {Id = (int) Permission.Read, Name = Read}
        };
    }
}