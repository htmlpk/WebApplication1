using BlackJack.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlackJack.DataAccessLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<GameRound> GameRounds { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserInGame> UserInGames { get; set; }

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
