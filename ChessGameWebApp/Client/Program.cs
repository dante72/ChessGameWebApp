using ChessGame;
using ChessGameWebApp.Client;
using ChessGameWebApp.Client.Services;
using AuthWebAPI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChessGameWebApp.Client.Models;
using Blazored.LocalStorage;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredModal();

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(sp => new AuthHttpClient { BaseAddress = new Uri("https://localhost:7256/") });
builder.Services.AddSingleton<IAuthWebApi, AuthWebApi>();
builder.Services.AddSingleton(b => new ChessBoard());
builder.Services.AddSingleton<IClientGameService, ClientGameService>();
builder.Services.AddScoped<IGameHubService, GameHubService>();
builder.Services.AddSingleton(user => new SiteUserInfo());
builder.Services.AddSingleton(updater => new TimeUpdater());
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthService, AuthService>();

await builder.Build().RunAsync();
