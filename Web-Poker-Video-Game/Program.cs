using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Web_Poker_Video_Game.Interfaces;
using Web_Poker_Video_Game.Pages;
using Web_Poker_Video_Game.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



// Add services to have an ability to use Api function through Web Application
builder.Services.AddHttpClient<IPlayerService, PlayerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7116/");
});

builder.Services.AddTransient<GameService>();
builder.Services.AddTransient<CardService>();
builder.Services.AddTransient<RankingService>();
builder.Services.AddTransient<GameHistoryService>();

// Register PokerCards component as a scoped service.
builder.Services.AddScoped<PokerCards>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

