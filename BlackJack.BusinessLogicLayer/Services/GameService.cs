﻿using BlackJack.BusinessLogicLayer.CardData;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer.Services
{

    public class GameService : IGameService
    {
        private const int CountOfStartCards = 2;
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

        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            var games = await _gameRepository.GetAll(userName);
            return games;
        }

        public async Task StartGame(string userName, int countOfBots)
        {
            var newGameGuid = await CreateGame();
            var gamers = await CreateUsers(newGameGuid, countOfBots, userName);
            await DealTwoStartCards(newGameGuid);
            await UpdateGameStatus(userName);
            await UpdateUsersStatus(userName);
        }

        public async Task<Guid> CreateGame()
        {
            Game newGame = new Game() { Date = DateTime.Now, IsFinished = false };
            await _gameRepository.Add(newGame);
            return newGame.Id;
        }


        public async Task<List<UserInGame>> CreateUsers(Guid newGameGuid, int countOfBots, string userName)
        {
            List<UserInGame> gamers = new List<UserInGame>();
            List<GameRound> cards = new List<GameRound>();
            string userId = await _userRepository.GetUserId(userName);
            List<string> botsIds = (await _userRepository.GetBotsIds()).ToList();
            for (var i = 0; i <= countOfBots; i++)
            {
                if (i == 0)
                {
                    gamers.Add(new UserInGame() { GameId = newGameGuid, Name = userName, IsDealer = false, IsFinished = false, GamerStatus = "InGame", Points = 0, UserId = userId });
                }
                if (i == 1)
                {
                    gamers.Add(new UserInGame() { GameId = newGameGuid, Name = "BotDealer", IsDealer = true, IsFinished = false, GamerStatus = "InGame", Points = 0, UserId = botsIds[5] });
                }
                if (i > 1)
                {
                    gamers.Add(new UserInGame() { GameId = newGameGuid, Name = "Bot" + i, IsDealer = false, IsFinished = false, GamerStatus = "InGame", Points = 0, UserId = botsIds[i - 2] });
                }
            }
            await _userRepository.Add(gamers);
            return gamers;
        }

        public async Task DealTwoStartCards(Guid gameId)
        {
            List<Card> usedCards = new List<Card>();
            List<GameRound> cardsToAdd = new List<GameRound>();
            var gamers = await _userRepository.FindByGameId(gameId);
            foreach (var gamer in gamers)
            {
                for (var i = 0; i < CountOfStartCards; i++)
                {
                    var newUsedCard = DealCardFromDeck(ref usedCards);
                    cardsToAdd.Add(new GameRound() { UserInGameId = gamer.Id, GameId = gamer.GameId, Points = newUsedCard.Points, Suit = newUsedCard.Suit.ToString(), Value = newUsedCard.Value.ToString(), RaundNumber = 0 });
                }
            }
            await _cardRepository.Add(cardsToAdd);
            await UpdateUsersPoints(gameId);
        }

        public Card DealCardFromDeck(ref List<Card> usedCards)
        {
            CardDeck deck = new CardDeck(true);
            return deck.DealCard(ref usedCards);
        }

        public async Task UpdateUsersPoints(Guid gameId)
        {
            var gamers = await _userRepository.FindByGameId(gameId);
            var cards = await _cardRepository.FindByGameId(gameId);
            List<UserInGame> gamersToUpdate = new List<UserInGame>();
            foreach (var gamer in gamers)
            {
                var gamerLastPoints = gamer.Points;
                var currentGamerPoints = 0;
                currentGamerPoints = cards.Where(item => item.UserInGameId == gamer.Id).Sum(item => item.Points);
                if (gamerLastPoints != currentGamerPoints)
                {
                    gamer.Points = currentGamerPoints;
                    gamersToUpdate.Add(gamer);
                }
            }
            await _userRepository.Update(gamersToUpdate);
        }

        public async Task<Match> GetLastMatch(string userName)
        {
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.Id);
            var cards = await _cardRepository.FindByGameId(game.Id);
            var match = new Match() { Game = game, Gamers = gamers, Rounds = cards };
            return match;
        }

        public async Task<Match> NextRound(string userName, bool isUserNeedCard)
        {
            var lastMatch = await GetLastMatch(userName);
            var raunds = await _cardRepository.FindByGameId(lastMatch.Game.Id);
            var cardsToAdd = new List<GameRound>();
            UserInGame user = null;
            List<Card> usedCards = MapRaundToCard(raunds);
            foreach (var gamer in lastMatch.Gamers)
            {
                if ((gamer.Name.Contains("Bot")) && (!gamer.IsFinished))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRaund = new GameRound() { GameId = lastMatch.Game.Id, Id = Guid.NewGuid(), Points = newCard.Points, Suit = newCard.Suit, Value = newCard.Value, UserInGameId = gamer.Id, RaundNumber = lastMatch.Game.CountOfRounds + 1 };
                    cardsToAdd.Add(newRaund);
                }
                if ((!gamer.Name.Contains("Bot")) && (!gamer.IsFinished) && (isUserNeedCard))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRaund = new GameRound() { GameId = lastMatch.Game.Id, Id = Guid.NewGuid(),
                        Points = newCard.Points, Suit = newCard.Suit, Value = newCard.Value, UserInGameId = gamer.Id,
                        RaundNumber = lastMatch.Game.CountOfRounds + 1 };
                    cardsToAdd.Add(newRaund);
                }
                if ((!gamer.Name.Contains("Bot")) && (!gamer.IsFinished) && (!isUserNeedCard))
                {
                    user = gamer; 
                }
            }

            if (user != null)
            {
                if ((!user.Name.Contains("Bot") && (!user.IsFinished) && (!isUserNeedCard)))
                {
                    user.IsFinished = true;
                    await _userRepository.Update(user);
                }
            }

            await _cardRepository.Add(cardsToAdd);
            await UpdateUsersPoints(lastMatch.Game.Id);
            await UpdateGameStatus(userName);
            await UpdateUsersStatus(userName);

            if (!isUserNeedCard && !lastMatch.Game.IsFinished)
            {
                await NextRound(userName, false);
                lastMatch = await GetLastMatch(userName);
            }
            return await GetLastMatch(userName);
        }

        public List<Card> MapRaundToCard(IEnumerable<GameRound> raunds)
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
            var gamers = await _userRepository.FindByGameId(game.Id);
            var countOfFinished = gamers.Where(data => data.IsFinished).Count();
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
            var gamers = await _userRepository.FindByGameId(game.Id);
            var dealerStatus = gamers.Where(data => data.IsDealer).Select(data => data.GamerStatus).First();
            var dealerPoints = gamers.Where(data => data.IsDealer).Select(data => data.Points).First();
            var maxGamerPoints = gamers.Select(data => data.Points).Where(data => data <= 21).Max();
            var handler = new GamersPointsHandler(gamers, dealerPoints, dealerStatus, maxGamerPoints);
            var usersToUpdate = new List<UserInGame>();
            if ((game.IsFinished)&&(dealerStatus.Equals("loser")))
            {
                handler.GameFinishedDealerloser(gamers, ref usersToUpdate, ref isUsersChanged);
            }
            if ((game.IsFinished) && (!dealerStatus.Equals("loser")))
            {
                handler.GameFinishedDealerNotloser(gamers, ref usersToUpdate, ref isUsersChanged);
            }
            if (!game.IsFinished)
            {
                handler.GameNotFinished(gamers, ref usersToUpdate, ref isUsersChanged);
            }
            if (isUsersChanged)
            {
                await _userRepository.Update(usersToUpdate);
                await UpdateGameStatus(userName);
            }
        }

        public async Task<Match> GetMatchById(Guid id)
        {
            var game = await _gameRepository.FindById(id.ToString());
            var gamers = await _userRepository.FindByGameId(game.Id);
            var cards = await _cardRepository.FindByGameId(game.Id);
            var match = new Match() { Game = game, Gamers = gamers, Rounds = cards };
            return match;
        }

    }
}








