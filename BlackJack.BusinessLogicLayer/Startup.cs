using BlackJack.DataAcessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using UI.Data.GameRepository;

namespace BlackJack.BusinessLogicLayer
{
    public static class Startup
    {
        public static void Configure(IServiceCollection serviceCollection,string connectionString)
        {
                     
        }

        public static void SetDapper(IServiceCollection serviceCollection, string connectionString)
        {
            // Repo Dapper
            serviceCollection.AddScoped<IGameRepository, GameRepository>(provider => new GameRepository(connectionString));
            serviceCollection.AddScoped<IUserRepository, UserInGameRepository>(provider => new UserInGameRepository(connectionString));
            serviceCollection.AddScoped<ICardRepository, GameRoundsRepository>(provider => new GameRoundsRepository(connectionString));
            // Services
            serviceCollection.AddScoped<IGameService, GameService>();
        }

        public static void SetEntityFramework(IServiceCollection serviceCollection, string connectionString)
        {
            // Repo EF
            serviceCollection.AddScoped<IGameRepository, GameRoundsEFRepository>();
            serviceCollection.AddScoped<IUserRepository, UserEFRepository>();
            serviceCollection.AddScoped<ICardRepository, CardEFRepository>();
            // Services
            serviceCollection.AddScoped<IGameService, GameService>();
        }
    }
}
