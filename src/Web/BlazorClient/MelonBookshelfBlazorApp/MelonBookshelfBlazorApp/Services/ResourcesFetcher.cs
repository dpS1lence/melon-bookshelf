using MelonBookshelfBlazorApp.ApiEndpoints;

namespace MelonBookshelfBlazorApp.Services
{
    public class ResourcesFetcher : ApiFetcher
    {
        private readonly ResourcesDataOptions _options;
        public ResourcesFetcher(HttpClient httpClient, BaseAdressOptions baseAdress, ResourcesDataOptions options) : base(httpClient, baseAdress)
        {
            _options = options;
        }

        public Task<string> Resources()
            => GetAsync(_options.Resources);

        public Task<string> SearchResources(HttpContent content)
            => PostAsync(_options.SearchResources, content);

        public Task<string> GetResourcesCategories()
            => GetAsync(_options.GetResourcesCategories);

        public Task<string> PhysicalResourceById(string resourceId)
            => GetAsync(string.Format(_options.PhysicalResourceById, resourceId));

        public Task<string> ResourceById(string resourceId)
            => GetAsync(string.Format(_options.ResourceById, resourceId));

        public Task<string> PhysicalResourcesByUserId(string userId)
            => GetAsync(string.Format(_options.PhysicalResourcesByUserId, userId));
    }
}
