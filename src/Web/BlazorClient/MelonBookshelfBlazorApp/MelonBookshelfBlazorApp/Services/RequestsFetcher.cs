using MelonBookshelfBlazorApp.ApiEndpoints;

namespace MelonBookshelfBlazorApp.Services
{
    public class RequestsFetcher : ApiFetcher
    {
        private readonly RequestsDataOptions _options;
        public RequestsFetcher(HttpClient httpClient, BaseAdressOptions baseAdress, RequestsDataOptions options) : base(httpClient, baseAdress)
        {
            _options = options;
        }

        public Task<string> AddRequest(HttpContent content)
            => PostAsync(_options.AddRequest, content);

        public Task<string> GetRequestsByUserId(string userId)
            => GetAsync(string.Format(_options.GetRequestsByUserId, userId));

        public Task<string> GetRequestById(string requestId)
            => GetAsync(string.Format(_options.GetRequestById, requestId));
    }
}
