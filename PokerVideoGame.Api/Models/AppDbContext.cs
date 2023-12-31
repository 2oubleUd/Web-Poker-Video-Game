﻿using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using PokerVideoGame.Models;
using Web_Poker_Video_Game.Services;

namespace PokerVideoGame.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        { 
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<GameHistory> GameHistories { get; set; }

        // it's for seeding DbSet by data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 1, Name = "Mikolaj", AccountBalance = 1000 });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 2, Name = "Bartosz", AccountBalance = 1000 });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 3, Name = "Kamil", AccountBalance = 1000 });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 4, Name = "Korneliusz", AccountBalance = 1000 });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 5, Name = "Amadeusz", AccountBalance = 1000 });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 6, Name = "Mateusz", AccountBalance = 1000 });

            modelBuilder.Entity<Player>().HasData(
                new Player { Id = 7, Name = "Szymon", AccountBalance = 1000 });

            modelBuilder.Entity<GameHistory>()
                .HasKey(g => g.Id); // Assuming Id is the primary key 

        }
    }
}
