using MelonBookshelfBlazorApp;
using MelonBookshelfBlazorApp.Services;
using MelonBookshelfBlazorApp.Services.Fetchers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient();

builder.Services.AddScoped(sp => new AuthenticationFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped(sp => new ResourcesFetcher(sp.GetRequiredService<HttpClient>()));
builder.Services.AddScoped<AuthenticationService>();

var app = builder.Build();

var authenticationService = app.Services.GetRequiredService<AuthenticationService>();
await authenticationService.InitializeClientToken();

await app.RunAsync();
