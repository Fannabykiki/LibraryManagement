using BookStore.Common.DTOs.User;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace BookStore.Extensions
{
    public static class ControllerExtensions
    {
        public static LoginResponse GetCurrentLoginUserId(this ControllerBase controller) 
        {
            if (controller.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var roleClaim = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var userIdString = identity?.FindFirst("UserId")?.Value;
                var userClaim = identity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(userIdString))
                {
                    return null;
                }

                var isUserIdValid = Guid.TryParse(userIdString, out Guid userId);

                if (!isUserIdValid)
                {
                    return null;
                }

                return new LoginResponse
                {
                    Role = roleClaim,
                    UserId = userId,
                    UserName = userClaim,
                };
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
