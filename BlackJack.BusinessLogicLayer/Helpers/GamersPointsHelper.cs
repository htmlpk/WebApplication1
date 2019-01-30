using BlackJack.DataAccessLayer.Entities;
using BlackJack.DataAcсessLayer.Enums;
using System.Collections.Generic;

namespace BlackJack.BusinessLogicLayer
{
    class GamersPointsHelper
    {
        private const int PointsToLoose = 22;
        private const int PointsToFinish = 17;
        private const int PointsToBlackJack = 21;
        private int _dealerPoints = 0;
        private int _maxGamerPoints = 0;
        private GamerStatus _dealerStatus;

        public GamersPointsHelper(IEnumerable<UserInGame> gamers, int dealerPoints, GamerStatus dealerStatus, int maxGamerPoints)
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
                    gamer.GamerStatus = GamerStatus.Winner;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points >= PointsToLoose) &&
                    (!gamer.IsFinished))
                {
                    gamer.IsFinished = true;    
                    gamer.GamerStatus = GamerStatus.Loser;
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
                    (gamer.GamerStatus != GamerStatus.Loser))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Loser;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == _maxGamerPoints) &&
                        gamer.GamerStatus != GamerStatus.Winner)
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Winner;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points > PointsToLoose) &&
                    (gamer.GamerStatus != GamerStatus.Loser))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Loser;
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
                (gamer.GamerStatus != GamerStatus.Loser))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Loser;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == _maxGamerPoints) &&
                    (gamer.Points > _dealerPoints) &&
                    (gamer.GamerStatus != GamerStatus.Winner) &&
                        (!gamer.Name.Contains("BotDealer")))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Winner;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Points == _maxGamerPoints) &&
                    (gamer.Points < _dealerPoints) &&
                    (gamer.GamerStatus != GamerStatus.Loser) &&
                    (!gamer.Name.Contains("BotDealer")))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Loser;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
                if ((gamer.Name.Contains("BotDealer")) &&
                    (gamer.Points == _maxGamerPoints) &&
                    (gamer.GamerStatus != GamerStatus.Winner))
                {
                    gamer.IsFinished = true;
                    gamer.GamerStatus = GamerStatus.Winner;
                    usersToUpdate.Add(gamer);
                    isUsersChanged = true;
                }
            }
        }
    }
}
