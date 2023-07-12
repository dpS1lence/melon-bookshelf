using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MelonBookshelfBlazorApp.Services
{
    public static class AuthenticationManager
    {
        private static bool isAuthenticated = false;
        private static bool isAdmin = false;
        public static bool IsAuthenticated { get => isAuthenticated; }
        public static bool IsAdmin { get => isAdmin; }

        public static AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public static async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }

        public static bool IsUserAdmin(IEnumerable<Claim> claims)
        {
            isAdmin = claims.Any(c => c.Value == "Admin");

            Console.WriteLine("is admin " + isAdmin);

            return isAdmin;
        }

        public static bool IsUserAuthenticated(AuthenticationState authState)
        {
            if(authState != null && authState.User != null && authState.User.Identity != null)
            {
                isAuthenticated = authState.User.Identity.IsAuthenticated;

                Console.WriteLine("is authenticated " + isAuthenticated);

                return isAuthenticated;
            }

            return false;
        }

        public static async Task UpdateAuthenticationState()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            isAuthenticated = IsUserAuthenticated(authState);
            isAdmin = IsUserAdmin(authState.User.Claims);

            Console.WriteLine($"is authenticated {isAuthenticated}, is admin {isAdmin}");
        }
    }
}
