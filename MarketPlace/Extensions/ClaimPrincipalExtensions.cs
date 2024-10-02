using System.Security.Claims;

namespace MarketPlace.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("AdminRole");
        }
    }
}
