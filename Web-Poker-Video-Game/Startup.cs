﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web_Poker_Video_Game.Interfaces;
using Web_Poker_Video_Game.Pages;
using Web_Poker_Video_Game.Services;
using Web_Poker_Video_Game.Services2;

namespace Web_Poker_Video_Game
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to have an ability to use Api function through Web Application
            services.AddHttpClient<IPlayerService, PlayerService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7116/");
            });

            

            services.AddTransient<GameService>();
            services.AddTransient<CardService>();
            services.AddTransient<IRankingService, RankingService>();
            services.AddTransient<GameHistoryService>();

            //services.AddScoped<PokerCards>();

        }

        public void Configure(IApplicationBuilder app)
        {
            // Add middleware components here
            app.UseHttpsRedirection();  // Redirect HTTP requests to HTTPS
            app.UseStaticFiles();       // Serve static files (e.g., images, CSS)
            app.UseRouting();           // Configure routing
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}