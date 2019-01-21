using System.Collections.Generic;
using System.Linq;
using UI.Entities;

namespace BlackJack.BusinessLogicLayer.Handlers
{
    class GamersPointsHandler
    {
        const int PointsToLoose = 22;
        const int PointsToFinish = 17;
        const int PointsToBlackJack = 21;

        int dealerPoints = 0;
        string dealerStatus = null;
        int maxGamerPoints = 0;

        public GamersPointsHandler(IEnumerable<UserInGame> gamers, int dealerPoints, string dealerStatus,int maxGamerPoints)
        {
            this.dealerPoints = dealerPoints;
            this.dealerStatus = dealerStatus;
            this.maxGamerPoints = maxGamerPoints;
        }

        public void GameNotFinished(ref List<UserInGame> usersToUpdate, ref bool isUsersChanged, IEnumerable<UserInGame> gamers)
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

        public void GameFinishedDealerlooser(ref List<UserInGame> usersToUpdate, ref bool isUsersChanged, IEnumerable<UserInGame> gamers)
        {
            foreach (var gamer in gamers)
            {
                if ((gamer.Points < maxGamerPoints) &&
                    (gamer.GamerStatus != "loser"))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == maxGamerPoints) &&
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

        public void GameFinishedDealerNotlooser(ref List<UserInGame> usersToUpdate, ref bool isUsersChanged, IEnumerable<UserInGame> gamers)
        {
            foreach (var gamer in gamers)
            {
                if ((gamer.Points < maxGamerPoints) &&
                (gamer.GamerStatus != "loser"))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = "loser";
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == maxGamerPoints) && (gamer.Points > dealerPoints) && (gamer.GamerStatus != "winner"))
                {
                    if (!gamer.Name.Contains("BotDealer"))
                    {
                        gamer.IsFinished = true;
                        gamer.GamerStatus = "winner";
                        usersToUpdate.Add(gamer);
                        isUsersChanged = true;
                    }
                }
                if ((gamer.Points == maxGamerPoints) && (gamer.Points < dealerPoints) && (gamer.GamerStatus != "loser"))
                {
                    if (!gamer.Name.Contains("BotDealer"))
                    {
                        gamer.IsFinished = true;
                        gamer.GamerStatus = "loser";
                        usersToUpdate.Add(gamer);
                        isUsersChanged = true;
                    }
                }
                if ((gamer.Name.Contains("BotDealer")) && (gamer.Points == maxGamerPoints) && (gamer.GamerStatus != "winner"))
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
