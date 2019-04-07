using System.Collections.Generic;
using TaskManager.Core.Enums;

namespace TaskManager.Core
{
    public static class Roles
    {
        public const string User = "User";
        public const string SiteAdministrator = "SiteAdministrator";

        public static IDictionary<Role, string> Dictionary = new Dictionary<Role, string>
            {
                {Role.User, User},
                {Role.SiteAdministrator, SiteAdministrator }
            };
    }
}