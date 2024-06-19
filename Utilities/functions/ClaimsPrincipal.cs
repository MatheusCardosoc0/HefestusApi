using System.Security.Claims;

namespace HefestusApi.Utilities.functions
{ 
    public static class ClaimsPrincipalExtensions
    {
        public static string GetSystemLocationId(this ClaimsPrincipal systemLocation)
        {
            return systemLocation?.Claims?.FirstOrDefault(c => c.Type == "id")?.Value;
        }

        public static string GetSystemLocationIdUser(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(c => c.Type == "systemLocationId")?.Value;
        }
    }
}
