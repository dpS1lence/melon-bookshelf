using MelonBookshelfBlazorApp.Services.Fetchers;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace MelonBookshelfBlazorApp.Services
{
    public class AuthenticationService : ApiFetcher
    {
        private readonly IJSRuntime _jsRuntime;

        public AuthenticationService(HttpClient httpClient, IJSRuntime jsRuntime) : base(httpClient)
        {
            _jsRuntime = jsRuntime;
        }

        public override void SetBearerToken(string token)
        {
            base.SetBearerToken(token);
            StoreToken(token);
        }

        public async Task InitializeClientToken()
        {
            var token = await GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public void StoreToken(string token)
        {
            _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwtToken", token);
        }

        public async Task<string> GetToken()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwtToken");
        }

        public static bool IsUserAdmin(string token)
        {
            if(token == null)
            {
                return false;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaims = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role);

            return roleClaims.Any(c => c.Value == "Admin");
        }

        public void RemoveToken()
        {
            _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwtToken");
            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
