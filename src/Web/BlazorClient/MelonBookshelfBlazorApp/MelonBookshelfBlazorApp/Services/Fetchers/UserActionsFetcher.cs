namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class UserActionsFetcher : ApiFetcher
    {
        public UserActionsFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> ReturnPhysicalResource(string resourceId, string userId, HttpContent content)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.ReturnPhysicalResource, resourceId, userId), content);

        public Task<string> GetPhysicalResource(string resourceId, string userId, HttpContent content)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.GetPhysicalResource, resourceId, userId), content);

        public Task<string> UpvoteRequest(string requestId, string userId, HttpContent content)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.UpvoteRequest, requestId, userId), content);

        public Task<string> FollowRequest(string requestId, string userId, HttpContent content)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.FollowRequest, requestId, userId), content);
    }
}
