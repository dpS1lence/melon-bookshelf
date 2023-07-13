using MelonBookshelfBlazorApp.ApiEndpoints;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MelonBookshelfBlazorApp.Services.Fetchers
{
    public abstract class ApiFetcher
    {
        protected readonly HttpClient httpClient;
        protected readonly string baseAddres = "http://localhost:5212/api/";

        protected ApiFetcher(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public virtual void SetBearerToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Invalid bearer token", nameof(token));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<string> GetAsync(string endpoint)
        {
            var response = await httpClient.GetAsync(baseAddres + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> PostAsync(string endpoint, HttpContent content)
        {
            try
            {
				var response = await httpClient.PostAsync(baseAddres + endpoint, content);
                await Console.Out.WriteLineAsync(response.Content.ToString());
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadAsStringAsync();
			}
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);

                throw ex;
            }
        }

        protected async Task<string> PutAsync(string endpoint, HttpContent content)
        {
            var response = await httpClient.PutAsync(baseAddres + endpoint, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> PutAsync(string endpoint)
        {
            var response = await httpClient.PutAsJsonAsync(baseAddres + endpoint, new {});
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> DeleteAsync(string endpoint)
        {
            var response = await httpClient.DeleteAsync(baseAddres + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
