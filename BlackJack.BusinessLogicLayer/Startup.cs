using BlackJack.DataAcessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using UI.Data.GameRepository;

namespace BlackJack.BusinessLogicLayer
{
    public static class Startup
    {
        public static void Configure(IServiceCollection serviceCollection,string connectionString)
        {
            // Repo Dapper
            //serviceCollection.AddTransient<IGameRepository, GameRepository>(provider => new GameRepository(connectionString));
            //serviceCollection.AddTransient<IUserRepository, UserRepository>(provider => new UserRepository(connectionString));
            //serviceCollection.AddTransient<ICardRepository, CardRepository>(provider => new CardRepository(connectionString));

            //// Repo EF
            serviceCollection.AddTransient<IGameRepository, GameEFRepository>();
            serviceCollection.AddTransient<IUserRepository, UserEFRepository>();
            serviceCollection.AddTransient<ICardRepository, CardEFRepository>();

            // Services
            serviceCollection.AddTransient<IGameService, GameService>();
        }
    }
}
