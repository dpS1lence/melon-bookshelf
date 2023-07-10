using MelonBookshelfBlazorApp;
using MelonBookshelfBlazorApp.ApiEndpoints;
using MelonBookshelfBlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

<<<<<<< Updated upstream
//builder.Configuration.AddJsonFile("api-endpoints.json");
=======
var baseAdress = builder.Configuration.GetSection("ApiEndpoints:BaseAdress").Get<BaseAdressOptions>();
var authentication = builder.Configuration.GetSection("ApiEndpoints:Authentication").Get<AuthenticationOptions>();
var baseUserActions = builder.Configuration.GetSection("ApiEndpoints:BaseUserActions").Get<BaseUserActionsOptions>();
var hrActions = builder.Configuration.GetSection("ApiEndpoints:HRActions").Get<HRActionsOptions>();
var hrDashboard = builder.Configuration.GetSection("ApiEndpoints:HRDashboard").Get<HRDashboardOptions>();
var requestsData = builder.Configuration.GetSection("ApiEndpoints:RequestsData").Get<RequestsDataOptions>();
var resourcesData = builder.Configuration.GetSection("ApiEndpoints:ResourcesData").Get<ResourcesDataOptions>();
>>>>>>> Stashed changes

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => new AuthenticationFetcher(sp.GetRequiredService<HttpClient>(), baseAdress, authentication));
//builder.Services.AddScoped<HRActionsFetcher>();
//builder.Services.AddScoped<HRDashboardFetcher>();
//builder.Services.AddScoped<RequestsFetcher>();
//builder.Services.AddScoped<ResourcesFetcher>();
//builder.Services.AddScoped<UserActionsFetcher>();

var app = builder.Build();

await app.RunAsync();
