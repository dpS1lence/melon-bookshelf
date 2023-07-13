using MelonBookshelfBlazorApp;
using MelonBookshelfBlazorApp.Services;
using MelonBookshelfBlazorApp.Services.Fetchers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient();

builder.Services.AddScoped(sp => new AuthenticationFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped(sp => new ResourcesFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped(sp => new HRActionsFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped(sp => new HRDashboardFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped(sp => new RequestsFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped(sp => new UserActionsFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddAuthorizationCore();

var app = builder.Build();

await app.RunAsync();
