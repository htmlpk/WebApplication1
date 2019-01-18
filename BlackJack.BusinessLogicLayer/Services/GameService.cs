using AutoMapper;
using BlackJack.BusinessLogicLayer;
using BlackJack.DataAcessLayer.Data;
using BlackJack.DataAcessLayer.Repository;
using Dapper;
using Dapper.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using UI.Entities;

namespace UI.Data.GameRepository
{
    public class GameService : IGameService
    {


        const int PointsToLoose = 22;
        const int PointsToFinish = 17;
        const int PointsToBlackJack = 21;

        private IGameRepository _gameRepository;
        private IUserRepository _userRepository;
        private ICardRepository _cardRepository;

        public string ConnectionString { get; set; }

        public GameService(IGameRepository gameRepository, IUserRepository userRepository, ICardRepository cardRepository)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _cardRepository = cardRepository;
        }

        public async Task<List<Game>> GetAll(string userName)
        {
            var games = await _gameRepository.GetAll(userName);
            return games;
        }

        public async Task StartGame(string userName, int countOfBots)
        {

            var newGameGuid = Guid.NewGuid();
            await CreateGame(newGameGuid);
            var gamers = await CreateUsers(newGameGuid, countOfBots, userName);
            await DealTwoStartCards(userName);

            await UpdateGameStatus(userName);
            await UpdateUsersStatus(userName);
        }

        public async Task CreateGame(Guid newGameGuid)
        {
            Game newGame = new Game() { ID = newGameGuid, Data = DateTime.Now, IsFinished = false };
            await _gameRepository.Add(newGame);
        }


        public async Task<List<UserInGame>> CreateUsers(Guid newGameGuid, int countOfBots, string userName)
        {
            List<UserInGame> gamers = new List<UserInGame>();
            List<Raund> cards = new List<Raund>();

            string userId = await _userRepository.GetUserId(userName);
            List<string> botsIds = await _userRepository.GetBotsIds();

            for (var i = 0; i <= countOfBots; i++)
            {
                var ID = Guid.NewGuid();
                if (i == 0)
                {
                    // Creating user
                    gamers.Add(new UserInGame() { ID = ID, GameId = newGameGuid, Name = userName, IsDealer = false, IsFinished = false, GamerStatus = "InGame", Points = 0, UserId = userId });
                }
                if (i == 1)
                {
                    // Creating bot-dealer
                    gamers.Add(new UserInGame() { ID = ID, GameId = newGameGuid, Name = "BotDealer", IsDealer = true, IsFinished = false, GamerStatus = "InGame", Points = 0, UserId = botsIds[5] });
                }
                if (i > 1)
                {
                    // Creating bots
                    gamers.Add(new UserInGame() { ID = ID, GameId = newGameGuid, Name = "Bot" + i, IsDealer = false, IsFinished = false, GamerStatus = "InGame", Points = 0, UserId = botsIds[i - 2] });
                }
            }
            await _userRepository.Add(gamers);

            return gamers;
        }

        public async Task DealTwoStartCards(string userName)
        {
            List<Card> usedCards = new List<Card>();
            List<Raund> cardsToAdd = new List<Raund>();

            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.ID);

            foreach (var gamer in gamers)
            {
                for (var i = 0; i < 2; i++)
                {
                    var newUsedCard = DealCardFromDeck(ref usedCards);
                    cardsToAdd.Add(new Raund() { ID = Guid.NewGuid(), UserInGameId = gamer.ID, GameId = gamer.GameId, Points = newUsedCard.Points, Suit = newUsedCard.Suit.ToString(), Value = newUsedCard.Value.ToString(), RaundNumber = 0 });
                }

            }
            await _cardRepository.Add(cardsToAdd);


            await UpdateUsersPoints(userName);

        }

        public Card DealCardFromDeck(ref List<Card> usedCards)
        {
            CardDeck deck = new CardDeck(true);
            return deck.DealCard(ref usedCards);
        }

        public async Task UpdateUsersPoints(string userName)
        {
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.ID);

            foreach (var gamer in gamers)
            {
                var cards = await _cardRepository.FindByUserId(gamer.ID);
                gamer.Points = cards.Sum(item => item.Points);
            }
            try
            {
                await _userRepository.Update(gamers.ToList());
            }
            catch (Exception e)
            {

                throw;
            }

        }


        public async Task<Match> GetLastMatch(string userName)
        {
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.ID);
            var cards = await _cardRepository.FindByGameId(game.ID);
            var match = new Match() { Game = game, Gamers = gamers.ToList(), Cards = cards };
            return match;
        }


        public async Task<Match> NextRound(string userName, bool isUserNeedCard)
        {
            var lastMatch = await GetLastMatch(userName);
            var raunds = await _cardRepository.FindByGameId(lastMatch.Game.ID);
            var cardsToAdd = new List<Raund>();

            List<Card> usedCards = MapRaundToCard(raunds);

            foreach (var gamer in lastMatch.Gamers)
            {
                if ((gamer.Name.Contains("Bot")) && (!gamer.IsFinished))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRaund = new Raund() { GameId = lastMatch.Game.ID, ID = Guid.NewGuid(), Points = newCard.Points, Suit = newCard.Suit, Value = newCard.Value, UserInGameId = gamer.ID, RaundNumber = lastMatch.Game.CountOfRounds + 1 };
                    cardsToAdd.Add(newRaund);
                }

                if ((!gamer.Name.Contains("Bot")) && (!gamer.IsFinished) && (isUserNeedCard))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRaund = new Raund() { GameId = lastMatch.Game.ID, ID = Guid.NewGuid(), Points = newCard.Points, Suit = newCard.Suit, Value = newCard.Value, UserInGameId = gamer.ID, RaundNumber = lastMatch.Game.CountOfRounds + 1 };
                    cardsToAdd.Add(newRaund);
                }


                if ((!gamer.Name.Contains("Bot")) && (!gamer.IsFinished) && (!isUserNeedCard))
                {
                    gamer.IsFinished = true;
                    await _userRepository.Update(gamer);
                }


            }
            await _cardRepository.Add(cardsToAdd);
            await UpdateUsersPoints(userName);
            await UpdateGameStatus(userName);
            await UpdateUsersStatus(userName);


            if (!isUserNeedCard)
                if (lastMatch.Game.IsFinished)
                {
                    await NextRound(userName, false);
                    lastMatch = await GetLastMatch(userName);
                }

            return await GetLastMatch(userName);

        }

        public List<Card> MapRaundToCard(List<Raund> raunds)
        {
            var cardList = new List<Card>();

            foreach (var raund in raunds)
            {
                cardList.Add(new Card(raund.Value, raund.Suit));
            }
            return cardList;
        }

        public async Task UpdateGameStatus(string userName)
        {
            bool isGameChanged = false;
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.ID);

            var countOfFinished = gamers.Where(data => data.IsFinished).Count();



            if (gamers.Where(data => !data.Name.Contains("Bot")).Select(data => data.IsFinished).First())
            {
                isGameChanged = true;
            }

            if (gamers.Count() == countOfFinished)
            {
                isGameChanged = true;
            }

            if (isGameChanged)
            {

                game.IsFinished = true;
                await _gameRepository.Update(game);
                await UpdateUsersStatus(userName);
            }
        }


        public async Task UpdateUsersStatus(string userName)
        {
            bool isUsersChanged = false;
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.ID);


            var dealerPoints = gamers.Where(data => data.IsDealer).Select(data => data.Points).First();
            var dealerStatus = gamers.Where(data => data.IsDealer).Select(data => data.GamerStatus).First();
            var maxGamerPoints = gamers.Select(data => data.Points).Where(data => data <= 21).Max();

            var userdToUpdate = new List<UserInGame>();
            switch (game.IsFinished)
            {
                // Game is not finished
                case false:


                    foreach (var gamer in gamers)
                    {
                        if ((gamer.Points == PointsToBlackJack) &&
                            (!gamer.IsFinished))
                        {

                            gamer.IsFinished = true;
                            gamer.GamerStatus = "winner";
                            userdToUpdate.Add(gamer);
                            isUsersChanged = true;
                        }

                        if ((gamer.Points >= PointsToLoose) &&
                            (!gamer.IsFinished))
                        {
                            gamer.IsFinished = true;
                            gamer.GamerStatus = "loser";
                            userdToUpdate.Add(gamer);
                            isUsersChanged = true;

                        }

                        if ((gamer.Points >= PointsToFinish) &&
                            (!gamer.IsFinished) &&
                            (gamer.Name.Contains("Bot")))
                        {
                            gamer.IsFinished = true;
                            userdToUpdate.Add(gamer);
                            isUsersChanged = true;
                        }





                    }
                    break;

                // Game is finished
                case true:

                    switch (dealerStatus)
                    {
                        case "loser":

                            {
                                foreach (var gamer in gamers)
                                {
                                    if ((gamer.Points < maxGamerPoints) &&
                                        (gamer.GamerStatus != "loser"))
                                    {
                                        gamer.IsFinished = true;
                                        gamer.GamerStatus = "loser";
                                        userdToUpdate.Add(gamer);
                                        isUsersChanged = true;
                                    }

                                    if ((gamer.Points == maxGamerPoints) &&
                                            gamer.GamerStatus != "winner")
                                    {
                                        gamer.IsFinished = true;
                                        gamer.GamerStatus = "winner";
                                        userdToUpdate.Add(gamer);
                                        isUsersChanged = true;
                                    }

                                    if ((gamer.Points > PointsToLoose) &&
                                        (gamer.GamerStatus != "loser"))
                                    {
                                        gamer.IsFinished = true;
                                        gamer.GamerStatus = "loser";
                                        userdToUpdate.Add(gamer);
                                        isUsersChanged = true;
                                    }

                                }

                            }
                            break;

                        default:

                            foreach (var gamer in gamers)
                            {
                                if ((gamer.Points < maxGamerPoints) &&
                                (gamer.GamerStatus != "loser"))
                                {
                                    gamer.IsFinished = true;
                                    gamer.GamerStatus = "loser";
                                    userdToUpdate.Add(gamer);
                                    isUsersChanged = true;
                                }

                                if ((gamer.Points == maxGamerPoints) && (gamer.Points > dealerPoints) && (gamer.GamerStatus != "winner"))
                                {
                                    if (!gamer.Name.Contains("BotDealer"))
                                    {
                                        gamer.IsFinished = true;
                                        gamer.GamerStatus = "winner";
                                        userdToUpdate.Add(gamer);
                                        isUsersChanged = true;
                                    }

                                }

                                if ((gamer.Points == maxGamerPoints) && (gamer.Points < dealerPoints) && (gamer.GamerStatus != "loser"))
                                {
                                    if (!gamer.Name.Contains("BotDealer"))
                                    {
                                        gamer.IsFinished = true;
                                        gamer.GamerStatus = "loser";
                                        userdToUpdate.Add(gamer);
                                        isUsersChanged = true;
                                    }

                                }

                                if ((gamer.Name.Contains("BotDealer")) && (gamer.Points == maxGamerPoints) && (gamer.GamerStatus != "winner"))
                                {
                                    gamer.IsFinished = true;
                                    gamer.GamerStatus = "winner";
                                    userdToUpdate.Add(gamer);
                                    isUsersChanged = true;
                                }



                            }
                            break;





                    }

                    break;



                default:

                    break;
            }
            if (isUsersChanged)
            {
                await _userRepository.Update(userdToUpdate);
                await UpdateGameStatus(userName);
            }

        }

        public async Task<Match> GetMatchById(Guid id)
        {
            var game = await _gameRepository.FindById(id.ToString());
            var gamers = await _userRepository.FindByGameId(game.ID);
            var cards = await _cardRepository.FindByGameId(game.ID);
            var match = new Match() { Game = game, Gamers = gamers, Cards = cards };
            return match;
        }
    }
}
         







