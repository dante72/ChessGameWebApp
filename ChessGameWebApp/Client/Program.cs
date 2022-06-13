using ChessGame;
using ChessGameWebApp.Client;
using ChessGameWebApp.Client.Services;
using ChessWebAPI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new ServerWebApi(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }));
builder.Services.AddSingleton(b => new ChessBoard());
builder.Services.AddSingleton<IClientGameService, ClientGameService>();
builder.Services.AddSingleton<IGameHubService, GameHubService>();

await builder.Build().RunAsync();
