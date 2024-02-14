using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokerVideoGame.Api.Controllers;
using PokerVideoGame.Api.Models;
using PokerVideoGame.Models.Data.Settings;
using System.Configuration;
using System.Data.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();

//builder.Services.AddScoped<IPlayerRepository, PlayerRepository>(); // I needed to add it from Startup.cs

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "BlazorCors",
        policy =>
        {
            policy.WithOrigins("https://localhost:7240")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection(nameof(TokenSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("BlazorCors");

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting(); // If you want to explicitly enable endpoint routing

app.MapControllers();


app.Run();
