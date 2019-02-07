using BlackJack.DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BlackJack.DataAccessLayer
{
    public static class Startup
    {
        public static void Configure(IServiceCollection serviceCollection,string connectionString)
        {
        }

        public static void SetDapper(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddScoped<IGameRepository, GameRepository>(provider => new GameRepository(connectionString));
            serviceCollection.AddScoped<IUserRepository, UserInGameRepository>(provider => new UserInGameRepository(connectionString));
            serviceCollection.AddScoped<ICardRepository, GameRoundRepository>(provider => new GameRoundRepository(connectionString));
        }

        public static void SetEntityFramework(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddScoped<IGameRepository, GameEFRepository>();
            serviceCollection.AddScoped<IUserRepository, UserEFRepository>();
            serviceCollection.AddScoped<ICardRepository, GameRoundEFRepository>();
        }
    }
}
