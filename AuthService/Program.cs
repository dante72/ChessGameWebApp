using AuthService.Services;
using DbContextDao;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConfig = builder.Configuration.GetSection("MySqlConfig");

builder.Services.AddDbContext<AuthContext>(
    options => options.UseMySql(
                 $@"server={dbConfig["Server"]};
                   port={dbConfig["Port"]};
                   user={dbConfig["User"]};
                   password={dbConfig["Password"]};
                   database={dbConfig["Database"]};",
                 new MySqlServerVersion(dbConfig["Version"]))
    );

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEF>();
builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
