using MelonBookshelfBlazorApp;
using MelonBookshelfBlazorApp.ApiEndpoints;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("api-endpoints.json");

var authentication = builder.Configuration.GetSection("ApiEndpoints").Get<Authentication>();
var baseUserActions = builder.Configuration.GetSection("ApiEndpoints").Get<BaseUserActions>();
var hrActions = builder.Configuration.GetSection("ApiEndpoints").Get<HRActions>();
var hrDashboard = builder.Configuration.GetSection("ApiEndpoints").Get<HRDashboard>();
var requestsData = builder.Configuration.GetSection("ApiEndpoints").Get<RequestsData>();
var resourcesData = builder.Configuration.GetSection("ApiEndpoints").Get<ResourcesData>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
