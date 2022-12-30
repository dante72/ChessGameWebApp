using ChessGame;
using ChessGameWebApp.Client;
using ChessGameWebApp.Client.Services;
using AuthWebAPI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChessGameWebApp.Client.Models;
using Blazored.Modal;
using ChessGameWebApp.Shared;
using ChessGameWebApp.Client.Services.Impl;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredModal();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new AuthHttpClient { BaseAddress = new Uri("https://localhost:7256/") });
builder.Services.AddScoped<IAuthWebApi, AuthWebApi>();
builder.Services.AddScoped(b => new ChessBoard());
builder.Services.AddScoped<IClientGameService, ClientGameServiceImpl>();
builder.Services.AddScoped<IGameHubService, GameHubServiceImplV2>();
builder.Services.AddScoped<IChatHubService, ChatHubServiceImpl>();
builder.Services.AddScoped(user => new SiteUserInfo());
builder.Services.AddScoped(updater => new TimeUpdater());
builder.Services.AddScoped<IMyLocalStorageService, MyLocalStorageServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImplV2>();
builder.Services.AddScoped(chat => new List<ChatMessage>());

await builder.Build().RunAsync();
