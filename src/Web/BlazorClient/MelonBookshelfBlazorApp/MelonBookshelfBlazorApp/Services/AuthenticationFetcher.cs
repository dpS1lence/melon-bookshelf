using MelonBookshelfBlazorApp.ApiEndpoints;

namespace MelonBookshelfBlazorApp.Services
{
    public class AuthenticationFetcher : ApiFetcher
    {
        private readonly AuthenticationOptions _authenticationOptions;
        public AuthenticationFetcher(HttpClient httpClient, BaseAdressOptions baseAdress, AuthenticationOptions authentication) : base(httpClient, baseAdress)
        {
            _authenticationOptions = authentication;
        }

        public Task<string> Register(HttpContent content)
            => PostAsync(_authenticationOptions.Register, content);

        public Task<string> Login(HttpContent content)
            => PostAsync(_authenticationOptions.Login, content);
    }
}
