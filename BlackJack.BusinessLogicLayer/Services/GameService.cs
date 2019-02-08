using AutoMapper;
using BlackJack.BusinessLogicLayer.CardData;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.DataAccessLayer.Repository;
using BlackJack.DataAcсessLayer.Enums;
using BlackJack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer.Services
{
    public class GameService : IGameService
    {
        private const string BotNamePart = "Bot";
        private const string BotDealerNamePart = "BotDealer";
        private const int PointsToNotLose = 21;
        private const int CountOfStartCards = 2;
        private IMapper _mapper;
        private IGameRepository _gameRepository;
        private IUserRepository _userRepository;
        private IRoundRepository _roundRepository;
        public string ConnectionString { get; set; }

        public GameService(IGameRepository gameRepository, IUserRepository userRepository, IRoundRepository roundRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _roundRepository = roundRepository;
        }

        public async Task StartGame(StartGameViewModel model)
        {
            var newGameGuid = await CreateGame();
            var gamers = await CreateGamers(newGameGuid, model.CountOfBots, model.UserName);
            await DealTwoStartCards(newGameGuid);
            await UpdateGameStatus(model.UserName);
            await UpdateUsersStatus(model.UserName);
        }

        public async Task<MatchViewModel> GetLastMatch(string userName)
        {
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.Id);
            var rounds = await _roundRepository.FindByGameId(game.Id);
            var match = new MatchViewModel() { Game = _mapper.Map(game, new GameViewModel()), Gamers = _mapper.Map(gamers, new List<UserInGameViewModel>()), Rounds = _mapper.Map(rounds, new List<RoundViewModel>()) };
            return match;
        }

        public async Task<MatchViewModel> NextRound(string userName, bool isUserNeedCard)
        {
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.Id);
            var rounds = await _roundRepository.FindByGameId(game.Id);
            var roundsToAdd = new List<GameRound>();
            UserInGame user = null;
            List<Card> usedCards = MapRoundToCard(rounds);
            List<UserInGame> usersToUpdatePoints = new List<UserInGame>();
            foreach (var gamer in gamers)
            {
                if ((gamer.Name.Contains(BotNamePart)) && (!gamer.IsFinished))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRound = new GameRound()
                    {
                        GameId = game.Id,
                        Points = newCard.Points,
                        Suit = newCard.Suit,
                        Value = newCard.Value,
                        UserInGameId = gamer.Id,
                        RoundNumber = game.CountOfRounds + 1
                    };
                    roundsToAdd.Add(newRound);
                    usersToUpdatePoints.Add(_mapper.Map(gamer, new UserInGame()));
                }
                if ((!gamer.Name.Contains(BotNamePart)) && (!gamer.IsFinished) && (isUserNeedCard))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRound = new GameRound()
                    {
                        GameId = game.Id,
                        Id = Guid.NewGuid(),
                        Points = newCard.Points,
                        Suit = newCard.Suit,
                        Value = newCard.Value,
                        UserInGameId = gamer.Id,
                        RoundNumber = game.CountOfRounds + 1
                    };
                    roundsToAdd.Add(newRound);
                    usersToUpdatePoints.Add(_mapper.Map(gamer, new UserInGame()));
                }
                if ((!gamer.Name.Contains(BotNamePart)) && (!gamer.IsFinished) && (!isUserNeedCard||gamer.Points> PointsToNotLose))
                {
                    user = _mapper.Map(gamer, new UserInGame());
                }
            }
            if (user != null && (!user.IsFinished) && (!isUserNeedCard))
            {
                user.IsFinished = true;
                await _userRepository.Update(user);
            }
            await _roundRepository.Add(roundsToAdd);
            await UpdateUsersPoints(usersToUpdatePoints, game.Id);
            await UpdateGameStatus(userName);
            await UpdateUsersStatus(userName);

            if ((!isUserNeedCard || (user != null && user.IsFinished==true)) && game.Status != GameStatus.Finished)
            {
                await NextRound(userName, false);
            }
            return await GetLastMatch(userName);
        }

        private async Task<Guid> CreateGame()
        {
            Game newGame = new Game() { Date = DateTime.Now, Status = GameStatus.NotFinished };
            await _gameRepository.Add(newGame);
            return newGame.Id;
        }

        private async Task<List<UserInGame>> CreateGamers(Guid newGameGuid, int countOfBots, string userName)
        {
            List<UserInGame> gamers = new List<UserInGame>();
            string userId = await _userRepository.GetUserId(userName);
            var botsAndDealer = await _userRepository.GetBotsAndDealer(countOfBots);
            gamers.Add(new UserInGame()
            {
                GameId = newGameGuid,
                Name = userName,
                IsDealer = false,
                IsFinished = false,
                GamerStatus = GamerStatus.InGame,
                Points = 0,
                UserId = userId
            });
            gamers.Add(new UserInGame()
            {
                GameId = newGameGuid,
                Name = BotDealerNamePart,
                IsDealer = true,
                IsFinished = false,
                GamerStatus = GamerStatus.InGame,
                Points = 0,
                UserId = botsAndDealer.dealer?.Id
            });
            foreach (var bot in botsAndDealer.bots)
            {
                gamers.Add(new UserInGame()
                {
                    GameId = newGameGuid,
                    Name = bot?.Email,
                    IsDealer = false,
                    IsFinished = false,
                    GamerStatus = GamerStatus.InGame,
                    Points = 0,
                    UserId = bot?.Id
                });
            }
            await _userRepository.Add(gamers);
            return gamers;
        }

        private async Task DealTwoStartCards(Guid gameId)
        {
            List<Card> usedCards = new List<Card>();
            List<GameRound> roundsToAdd = new List<GameRound>();
            var gamers = await _userRepository.FindByGameId(gameId);
            foreach (var gamer in gamers)
            {
                for (var i = 0; i < CountOfStartCards; i++)
                {
                    var newUsedCard = DealCardFromDeck(ref usedCards);
                    roundsToAdd.Add(new GameRound() { UserInGameId = gamer.Id, GameId = gamer.GameId, Points = newUsedCard.Points, Suit = newUsedCard.Suit.ToString(), Value = newUsedCard.Value.ToString(), RoundNumber = 0 });
                }
            }
            await _roundRepository.Add(roundsToAdd);
            await UpdateUsersPoints(gamers, gameId);
        }

        private Card DealCardFromDeck(ref List<Card> usedCards)
        {
            CardDeck deck = new CardDeck(true);
            return deck.DealCard(ref usedCards);
        }

        private async Task UpdateUsersPoints(IEnumerable<UserInGame> gamers, Guid gameId)
        {
            var rounds = await _roundRepository.FindByGameId(gameId);
            List<UserInGame> gamersToUpdate = new List<UserInGame>();
            foreach (var gamer in gamers)
            {
                var gamerLastPoints = gamer.Points;
                var currentGamerPoints = 0;
                currentGamerPoints = rounds.Where(item => item.UserInGameId == gamer.Id).Sum(item => item.Points);
                if (gamerLastPoints != currentGamerPoints)
                {
                    gamer.Points = currentGamerPoints;
                    gamersToUpdate.Add(gamer);
                }
            }
            await _userRepository.Update(gamersToUpdate);
        }

        private List<Card> MapRoundToCard(IEnumerable<GameRound> rounds)
        {
            var cardList = new List<Card>();

            foreach (var round in rounds)
            {
                cardList.Add(new Card(round.Value, round.Suit));
            }
            return cardList;
        }

        private async Task UpdateGameStatus(string userName)
        {
            bool isGameFinished = false;
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.Id);
            var countOfFinished = gamers.Where(data => data.IsFinished).Count();
            if (gamers.Count() == countOfFinished)
            {
                isGameFinished = true;
            }
            if (isGameFinished)
            {
                game.Status = GameStatus.Finished;
                await _gameRepository.Update(game);
                await UpdateUsersStatus(userName);
            }
        }

        private async Task UpdateUsersStatus(string userName)
        {
            bool isUsersChanged = false;
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.Id);
            var dealerStatus = gamers.Where(data => data.IsDealer).Select(data => data.GamerStatus).FirstOrDefault();
            var dealerPoints = gamers.Where(data => data.IsDealer).Select(data => data.Points).FirstOrDefault();
            var gamerPointsLessThen21 = gamers.Select(data => data.Points).Where(data => data <= PointsToNotLose);
            int maxGamerPoints = 0;
            if (gamerPointsLessThen21 != null && gamerPointsLessThen21.Count() != 0)
            {
                maxGamerPoints = gamerPointsLessThen21.Max();
            }
            var handler = new GamersPointsHelper(gamers, dealerPoints, dealerStatus, maxGamerPoints);
            var usersToUpdate = new List<UserInGame>();
            if ((game.Status == GameStatus.Finished) && (dealerStatus == GamerStatus.Loser))
            {
                handler.GameFinishedDealerLoser(gamers, ref usersToUpdate, ref isUsersChanged);
            }
            if ((game.Status == GameStatus.Finished) && (dealerStatus != GamerStatus.Loser))
            {
                handler.GameFinishedDealerNotLoser(gamers, ref usersToUpdate, ref isUsersChanged);
            }
            if (game.Status != GameStatus.Finished)
            {
                handler.GameNotFinished(gamers, ref usersToUpdate, ref isUsersChanged);
            }
            if (isUsersChanged)
            {
                await _userRepository.Update(usersToUpdate);
                await UpdateGameStatus(userName);
            }
        }
    }
}








