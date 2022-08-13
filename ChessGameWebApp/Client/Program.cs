using ChessGame;
using ChessGameWebApp.Client;
using ChessGameWebApp.Client.Services;
using AuthWebAPI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new AuthHttpClient { BaseAddress = new Uri("https://localhost:7256/") });
builder.Services.AddScoped<IAuthWebApi, AuthWebApi>();

builder.Services.AddSingleton(b => new ChessBoard());
builder.Services.AddSingleton<IClientGameService, ClientGameService>();
builder.Services.AddSingleton<IGameHubService, GameHubService>();
builder.Services.AddSingleton(user => new SiteUserInfo());

await builder.Build().RunAsync();
