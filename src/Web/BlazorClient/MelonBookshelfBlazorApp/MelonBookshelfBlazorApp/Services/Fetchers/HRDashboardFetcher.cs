﻿namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public class HRDashboardFetcher : ApiFetcher
    {
        public HRDashboardFetcher(HttpClient httpClient) : base(httpClient)
        { }

        public Task<string> GetRequests()
            => GetAsync(ApiEndpoints.ApiEndpoints.HRDashboard.GetRequests);

        public Task<string> GetAllResourcesForHR()
            => GetAsync(ApiEndpoints.ApiEndpoints.HRDashboard.GetAllResourcesForHR);

        public Task<string> AddResource(HttpContent content)
            => PostAsync(ApiEndpoints.ApiEndpoints.HRDashboard.AddResource, content);
    }
}
