using MelonBookshelfBlazorApp.ApiEndpoints;

namespace MelonBookshelfBlazorApp.Services
{
    public class HRDashboardFetcher : ApiFetcher
    {
        private readonly HRDashboardOptions _options;
        public HRDashboardFetcher(HttpClient httpClient, BaseAdressOptions baseAdress, HRDashboardOptions options) : base(httpClient, baseAdress)
        {
            _options = options;
        }

        public Task<string> GetRequests()
            => GetAsync(_options.GetRequests);

        public Task<string> GetAllResourcesForHR()
            => GetAsync(_options.GetAllResourcesForHR);

        public Task<string> AddResource(HttpContent content)
            => PostAsync(_options.AddResource, content);
    }
}
