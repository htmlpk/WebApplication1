using BlackJack.ViewModels;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer
{
    public interface IGameService
    {
        Task StartGame(StartGameViewModel model);
        Task<MatchViewModel> NextRound(string userName, bool isUserNeedCard);
        Task<MatchViewModel> GetLastMatch(string userName);
    }
}
