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
            serviceCollection.AddTransient<IGameRepository, GameRepository>(provider => new GameRepository(connectionString));
            serviceCollection.AddTransient<IUserRepository, UserInGameRepository>(provider => new UserInGameRepository(connectionString));
            serviceCollection.AddTransient<ICardRepository, GameRoundsRepository>(provider => new GameRoundsRepository(connectionString));
            // Services
            serviceCollection.AddTransient<IGameService, GameService>();
        }

        public static void SetEntityFramework(IServiceCollection serviceCollection, string connectionString)
        {
            // Repo EF
            serviceCollection.AddTransient<IGameRepository, GameRoundsEFRepository>();
            serviceCollection.AddTransient<IUserRepository, UserEFRepository>();
            serviceCollection.AddTransient<ICardRepository, CardEFRepository>();
            // Services
            serviceCollection.AddTransient<IGameService, GameService>();
        }
    }
}
