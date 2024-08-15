using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PokerVideoGame.Api.Controllers;
using PokerVideoGame.Api.Data;
using PokerVideoGame.Api.Repositories;
using PokerVideoGame.Models.Data.Settings;
using System.Configuration;
using System.Data.Common;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        var cardRepository = services.GetRequiredService<ICardRepository>();
        await DbInitializer.Initialize(context, cardRepository);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex.Message, "An error has occured seeding the DB");
    }
}

app.Run();
