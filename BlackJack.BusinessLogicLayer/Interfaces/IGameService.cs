using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer
{
    public interface IGameService
    {
        Task StartGame(string userName, int countOfBots);
        Task<Match> NextRound(string userName, bool isUserNeedCard);
        Task<Match> GetLastMatch(string userName);
    }
}
