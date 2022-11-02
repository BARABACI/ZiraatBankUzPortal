using System.Security.Claims;

namespace ZiraatBankUzPortal.Client.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public static string GetName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name).Value;
        }
        public static string GetRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role).Value;
        }

    }
}