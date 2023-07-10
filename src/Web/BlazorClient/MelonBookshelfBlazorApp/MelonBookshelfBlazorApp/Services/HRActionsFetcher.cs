using MelonBookshelfBlazorApp.ApiEndpoints;

namespace MelonBookshelfBlazorApp.Services
{
    public class HRActionsFetcher : ApiFetcher
    {
        private readonly HRActionsOptions _options;
        public HRActionsFetcher(HttpClient httpClient, BaseAdressOptions baseAdress, HRActionsOptions options) : base(httpClient, baseAdress)
        {
            _options = options;
        }

        public Task<string> ConfirmRequest(string requestId, HttpContent content)
            => PutAsync(string.Format(_options.ConfirmRequest, requestId), content);

        public Task<string> RejectRequest(string requestId, HttpContent content)
            => PutAsync(string.Format(_options.RejectRequest, requestId), content);

        public Task<string> SetResourceInProgress(string resourceId, HttpContent content)
            => PutAsync(string.Format(_options.SetResourceInProgress, resourceId), content);

        public Task<string> SetResourceInDelivery(string resourceId, HttpContent content)
            => PutAsync(string.Format(_options.SetResourceInDelivery, resourceId), content);

        public Task<string> SetResourceDelivered(string resourceId, HttpContent content)
            => PutAsync(string.Format(_options.SetResourceDelivered, resourceId), content);
    }

}
