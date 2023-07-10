using MelonBookshelfBlazorApp.ApiEndpoints;

namespace MelonBookshelfBlazorApp.Services
{
    public class UserActionsFetcher : ApiFetcher
    {
        private readonly BaseUserActionsOptions _options;
        public UserActionsFetcher(HttpClient httpClient, BaseAdressOptions baseAdress, BaseUserActionsOptions options) : base(httpClient, baseAdress)
        {
            _options = options;
        }

        public Task<string> ReturnPhysicalResource(string resourceId, string userId, HttpContent content)
            => PutAsync(string.Format(_options.ReturnPhysicalResource, resourceId, userId), content);

        public Task<string> GetPhysicalResource(string resourceId, string userId, HttpContent content)
            => PutAsync(string.Format(_options.GetPhysicalResource, resourceId, userId), content);

        public Task<string> UpvoteRequest(string requestId, string userId, HttpContent content)
            => PutAsync(string.Format(_options.UpvoteRequest, requestId, userId), content);

        public Task<string> FollowRequest(string requestId, string userId, HttpContent content)
            => PutAsync(string.Format(_options.FollowRequest, requestId, userId), content);
    }
}
