namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class AuthenticationFetcher : ApiFetcher
    {
        public AuthenticationFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> Register(HttpContent content)
            => PostAsync(ApiEndpoints.ApiEndpoints.Authentication.Register, content);

        public Task<string> Login(HttpContent content)
            => PostAsync(ApiEndpoints.ApiEndpoints.Authentication.Login, content);
    }
}
