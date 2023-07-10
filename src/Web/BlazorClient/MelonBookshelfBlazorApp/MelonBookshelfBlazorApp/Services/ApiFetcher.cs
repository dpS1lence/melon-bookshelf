using MelonBookshelfBlazorApp.ApiEndpoints;
using System.Net.Http.Headers;

namespace MelonBookshelfBlazorApp.Services
{
    public abstract class ApiFetcher
    {
        protected readonly HttpClient httpClient;
        protected readonly BaseAdressOptions baseAddress;

        protected ApiFetcher(HttpClient httpClient, BaseAdressOptions baseAdress)
        {
            this.baseAddress = baseAdress;
            this.httpClient = httpClient;
        }

        public void SetBearerToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Invalid bearer token", nameof(token));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<string> GetAsync(string endpoint)
        {
            var response = await httpClient.GetAsync(baseAddress.Adress + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> PostAsync(string endpoint, HttpContent content)
        {
            var response = await httpClient.PostAsync(baseAddress.Adress + endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> PutAsync(string endpoint, HttpContent content)
        {
            var response = await httpClient.PutAsync(baseAddress.Adress + endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> DeleteAsync(string endpoint)
        {
            var response = await httpClient.DeleteAsync(baseAddress.Adress + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
