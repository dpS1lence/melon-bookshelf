namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class RequestsFetcher : ApiFetcher
    {
        public RequestsFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> AddRequest(HttpContent content)
            => PostAsync(ApiEndpoints.ApiEndpoints.RequestsData.AddRequest, content);

        public Task<string> GetRequestsByUserId(string userId)
            => GetAsync(string.Format(ApiEndpoints.ApiEndpoints.RequestsData.GetRequestsByUserId, userId));

        public Task<string> GetRequestById(string requestId)
            => GetAsync(string.Format(ApiEndpoints.ApiEndpoints.RequestsData.GetRequestById, requestId));
    }
}
