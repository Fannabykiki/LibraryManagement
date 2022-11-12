using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Extensions
{
    public static class ControllerExtensions
    {
        public static Guid? GetCurrentLoginUserId(this ControllerBase controller) 
        {
            if (controller.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userIdString = identity?.FindFirst("UserID")?.Value;

                if (string.IsNullOrWhiteSpace(userIdString))
                    return null;

                var isUserIdValid = Guid.TryParse(userIdString, out Guid userId);

                if (!isUserIdValid)
                    return null;

                return userId;
            }
            else
            {
                return null;
            }
        }

        public static string SayHello(this string currentString, string name)
        {
            return $"Hello : {name}";
        }

        public static string SayHello(this string currentString)
        {
            return $"Hello : {currentString ?? "Anomynous"}";
        }
    }
}
