using BlackJack.BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlackJack.BusinessLogicLayer
{
    public static class Startup
    {
        public static void Configure(IServiceCollection serviceCollection,string connectionString)
        {
        }

        public static void InjectServices(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddScoped<IGameService, GameService>();
            serviceCollection.AddScoped<IHistoryService, HistoryService>();
        }
    }
}
