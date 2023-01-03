using ChessGame;
using ChessGameWebApp.Client;
using ChessGameWebApp.Client.Services;
using ChessGameClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.Modal;
using ChessGameWebApp.Shared;
using ChessGameWebApp.Client.Services.Impl;
using ChessGameClient.AuthWebAPI;
using ChessGameClient.Services;
using ChessGameClient.Models;
using ChessGameClient.Services.Impl;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredModal();

builder.Services.AddScoped<GameHttpClient>();
builder.Services.AddScoped<AuthHttpClient>();
builder.Services.AddScoped<IAuthWebApi, AuthWebApi>();
builder.Services.AddScoped<ChessBoard>();
builder.Services.AddScoped<IClientGameService, ClientGameServiceImpl>();
builder.Services.AddScoped<IGameHubService, GameHubServiceImplV2>();
builder.Services.AddScoped<IChatHubService, ChatHubServiceImpl>();
builder.Services.AddScoped<SiteUserInfo>();
builder.Services.AddScoped<TimeUpdater>();
builder.Services.AddScoped<IMyLocalStorageService, MyLocalStorageServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImplV2>();
builder.Services.AddScoped<List<ChatMessage>>();

await builder.Build().RunAsync();
