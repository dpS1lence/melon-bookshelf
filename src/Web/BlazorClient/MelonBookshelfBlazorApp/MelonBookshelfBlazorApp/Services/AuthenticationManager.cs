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

        public static Action OnChange = delegate { };

        public static AuthenticationStateProvider AuthenticationStateProvider { private get; set; }

        public static async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await AuthenticationStateProvider.GetAuthenticationStateAsync();
        }

        public static bool IsUserAdmin(IEnumerable<Claim> claims)
        {
            foreach (var item in claims)
            {
                Console.WriteLine(item.Type + " " + item.Value);
            }
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

            OnChange.Invoke();
        }

        public static async Task<string> GetUserId(AuthenticationStateProvider authenticationStateProvider)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.First().Value;
            }

            throw new ArgumentException(nameof(user));
        }

        public static async Task<string> GetUserName(AuthenticationStateProvider authenticationStateProvider)
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
				foreach (var claim in user.Claims)
				{
					if (claim.Value.Contains('@'))
					{
						return claim.Value.Split('@').First();

					}
				}
			}

            throw new ArgumentException(nameof(user));
        }
    }
}
