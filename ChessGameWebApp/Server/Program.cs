using ChessGame;
using ChessGameWebApp.Server.Repository;
using ChessGameWebApp.Server.Services;
using ChessGameWebApp.Server.SignalRHub;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ChessGameWebApp.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddSingleton(b => new ChessBoard(true));
builder.Services.AddScoped<IServerGameService, ServerGameService>();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin());

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();

app.MapRazorPages();
//app.MapFallbackToFile("index.html");

app.MapHub<BroadcastHub>("/chathub");
app.MapHub<GameHub>("/gamehub");

app.Run();
