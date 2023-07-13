namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class UserActionsFetcher : ApiFetcher
    {
        public UserActionsFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> ReturnPhysicalResource(int resourceId, string userId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.ReturnPhysicalResource, resourceId, userId));

        public Task<string> GetPhysicalResource(int resourceId, string userId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.GetPhysicalResource, resourceId, userId));

        public Task<string> UpvoteRequest(int requestId, string userId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.UpvoteRequest, requestId, userId));

        public Task<string> FollowRequest(int requestId, string userId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.BaseUserActions.FollowRequest, requestId, userId));
    }
}
