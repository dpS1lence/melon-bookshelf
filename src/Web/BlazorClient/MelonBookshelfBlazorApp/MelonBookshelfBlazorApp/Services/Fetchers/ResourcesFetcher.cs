namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class ResourcesFetcher : ApiFetcher
    {
        public ResourcesFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> Resources()
            => GetAsync(ApiEndpoints.ApiEndpoints.ResourcesData.Resources);

        public Task<string> SearchResources(HttpContent content)
            => PostAsync(ApiEndpoints.ApiEndpoints.ResourcesData.SearchResources, content);

        public Task<string> GetResourcesCategories()
            => GetAsync(ApiEndpoints.ApiEndpoints.ResourcesData.GetResourcesCategories);

        public Task<string> PhysicalResourceById(string resourceId)
            => GetAsync(string.Format(ApiEndpoints.ApiEndpoints.ResourcesData.PhysicalResourceById, resourceId));

        public Task<string> ResourceById(string resourceId)
            => GetAsync(string.Format(ApiEndpoints.ApiEndpoints.ResourcesData.ResourceById, resourceId));

        public Task<string> PhysicalResourcesByUserId(string userId)
            => GetAsync(string.Format(ApiEndpoints.ApiEndpoints.ResourcesData.PhysicalResourcesByUserId, userId));
    }
}
