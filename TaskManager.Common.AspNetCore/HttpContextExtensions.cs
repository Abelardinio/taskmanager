using System;
using System.Security.Claims;

namespace TaskManager.Common.AspNetCore
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst("jti").Value);
        }
    }
}