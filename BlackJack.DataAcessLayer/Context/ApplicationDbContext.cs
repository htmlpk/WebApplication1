﻿using BlackJack.DataAcessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlackJack.DataAcessLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<GameRound> GameRound { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<UserInGame> UserInGame { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
