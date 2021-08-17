using System.Security.Claims;

namespace ArtPortfolio.Extensions
{
    public static class ClaimsPrincipleExtension
    {
        public static string Id(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string Username(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static bool IsAdmin(this ClaimsPrincipal principal)
        {
            return principal.IsInRole("Administrator");
        }
    }
}