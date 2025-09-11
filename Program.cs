using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookingAssistantWeb;
using BookingAssistantWeb.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<AppsettingsService>();
builder.Services.AddSingleton<AppState>();

builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
