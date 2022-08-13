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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

JwtConfig jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,

            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
//builder.Services.AddAuthorization();


builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddSingleton(b => new ChessBoard(true));
builder.Services.AddScoped<IServerGameService, ServerGameService>();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();



var app = builder.Build();

//app.UseAuthorization();


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

app.UseAuthentication();
app.UseAuthorization();

app.Run();
