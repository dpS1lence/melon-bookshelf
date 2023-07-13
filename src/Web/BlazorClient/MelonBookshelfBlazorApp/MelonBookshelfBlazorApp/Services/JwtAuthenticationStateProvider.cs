using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MelonBookshelfBlazorApp.Services
{
	public class JwtAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly IJSRuntime _jsRuntime;

		public JwtAuthenticationStateProvider(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public void NotifyStateChanged()
		{
			NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwtToken");

            if (string.IsNullOrEmpty(token))
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			var identity = new ClaimsIdentity(ParseClaims(jwtToken), "jwt");
			var user = new ClaimsPrincipal(identity);

			return new AuthenticationState(user);
		}

		private static IEnumerable<Claim> ParseClaims(JwtSecurityToken jwtToken)
		{
			var claims = new List<Claim>();

			foreach (var claim in jwtToken.Claims)
			{
				claims.Add(new Claim(claim.Type, claim.Value));
			}

			return claims;
		}
	}
}
