using BlackJack.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BlackJack.BusinessLogicLayer
{
    class GamersPointsHandler
    {
        private const int PointsToLoose = 22;
        private const int PointsToFinish = 17;
        private const int PointsToBlackJack = 21;
        private int _dealerPoints = 0;
        private string _dealerStatus = null;
        private int _maxGamerPoints = 0;

        public GamersPointsHandler(IEnumerable<UserInGame> gamers, int dealerPoints, string dealerStatus, int maxGamerPoints)
        {
            _dealerPoints = dealerPoints;
            _dealerStatus = dealerStatus;
            _maxGamerPoints = maxGamerPoints;
        }

        public void GameNotFinished(IEnumerable<UserInGame> gamers, ref List<UserInGame> usersToUpdate, ref bool isUsersChanged)
        {
            foreach (var gamer in gamers)
            {
                if ((gamer.Points == PointsToBlackJack) &&
                    (!gamer.IsFinished))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "winner";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points >= PointsToLoose) &&
                    (!gamer.IsFinished))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points >= PointsToFinish) &&
                    (!gamer.IsFinished) &&
                    (gamer.Name.Contains("Bot")))
                {
                    gamer.IsFinished = true;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
            }
        }

        public void GameFinishedDealerloser(IEnumerable<UserInGame> gamers, ref List<UserInGame> usersToUpdate, ref bool isUsersChanged)
        {
            foreach (var gamer in gamers)
            {
                if ((gamer.Points < _maxGamerPoints) &&
                    (gamer.GamerStatus != "loser"))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == _maxGamerPoints) &&
                        gamer.GamerStatus != "winner")
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "winner";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points > PointsToLoose) &&
                    (gamer.GamerStatus != "loser"))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
            }
        }

        public void GameFinishedDealerNotloser(IEnumerable<UserInGame> gamers, ref List<UserInGame> usersToUpdate, ref bool isUsersChanged)
        {
            foreach (var gamer in gamers)
            {
                if ((gamer.Points < _maxGamerPoints) &&
                (gamer.GamerStatus != "loser"))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == _maxGamerPoints) &&
                    (gamer.Points > _dealerPoints) &&
                    (gamer.GamerStatus != "winner") &&
                        (!gamer.Name.Contains("BotDealer")))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "winner";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == _maxGamerPoints) &&
                    (gamer.Points < _dealerPoints) &&
                    (gamer.GamerStatus != "loser") &&
                    (!gamer.Name.Contains("BotDealer")))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Name.Contains("BotDealer")) &&
                    (gamer.Points == _maxGamerPoints) &&
                    (gamer.GamerStatus != "winner"))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "winner";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
            }
        }
    }
}
