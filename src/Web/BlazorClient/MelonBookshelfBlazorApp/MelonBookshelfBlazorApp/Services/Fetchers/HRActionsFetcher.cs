namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class HRActionsFetcher : ApiFetcher
    {
        public HRActionsFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> ConfirmRequest(int requestId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.HRActions.ConfirmRequest, requestId));

        public Task<string> RejectRequest(int requestId, HttpContent content)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.HRActions.RejectRequest, requestId), content);

        public Task<string> SetRequestInProgress(int resourceId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.HRActions.SetRequestInProgress, resourceId));

        public Task<string> SetRequestInDelivery(int resourceId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.HRActions.SetRequestInDelivery, resourceId));

        public Task<string> SetRequestDelivered(int resourceId)
            => PutAsync(string.Format(ApiEndpoints.ApiEndpoints.HRActions.SetRequestDelivered, resourceId));
    }
}
