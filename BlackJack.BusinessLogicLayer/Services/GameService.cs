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
        private ICardRepository _cardRepository;
        public string ConnectionString { get; set; }

        public GameService(IGameRepository gameRepository, IUserRepository userRepository, ICardRepository cardRepository,IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _cardRepository = cardRepository;
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
            var cards = await _cardRepository.FindByGameId(game.Id);
            var match = new MatchViewModel() { Game = _mapper.Map(game, new GameViewModel()), Gamers = _mapper.Map(gamers, new List<UserInGameViewModel>()), Rounds = _mapper.Map(cards, new List<RoundViewModel>()) };
            return match;
        }

        public async Task<MatchViewModel> NextRound(string userName, bool isUserNeedCard)
        {
            var lastMatch = await GetLastMatchForNextRound(userName);
            var raunds = await _cardRepository.FindByGameId(lastMatch.Game.Id);
            var cardsToAdd = new List<GameRound>();
            UserInGame user = null;
            List<Card> usedCards = MapRaundToCard(raunds);
            List<UserInGame> usersToUpdatePoints = new List<UserInGame>();
            foreach (var gamer in lastMatch.Gamers)
            {
                if ((gamer.Name.Contains(BotNamePart)) && (!gamer.IsFinished))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRaund = new GameRound()
                    {
                        GameId = lastMatch.Game.Id,
                        Points = newCard.Points,
                        Suit = newCard.Suit,
                        Value = newCard.Value,
                        UserInGameId = gamer.Id,
                        RoundNumber = lastMatch.Game.CountOfRounds + 1
                    };
                    cardsToAdd.Add(newRaund);
                    usersToUpdatePoints.Add(_mapper.Map(gamer,new UserInGame()));
                }
                if ((!gamer.Name.Contains(BotNamePart)) && (!gamer.IsFinished) && (isUserNeedCard))
                {
                    var newCard = DealCardFromDeck(ref usedCards);
                    var newRaund = new GameRound()
                    {
                        GameId = lastMatch.Game.Id,
                        Id = Guid.NewGuid(),
                        Points = newCard.Points,
                        Suit = newCard.Suit,
                        Value = newCard.Value,
                        UserInGameId = gamer.Id,
                        RoundNumber = lastMatch.Game.CountOfRounds + 1
                    };
                    cardsToAdd.Add(newRaund);
                    usersToUpdatePoints.Add(_mapper.Map(gamer, new UserInGame()));
                }
                if ((!gamer.Name.Contains(BotNamePart)) && (!gamer.IsFinished) && (!isUserNeedCard))
                {
                    user = _mapper.Map(gamer, new UserInGame());
                }
            }

            if (user != null)
            {
                if ((!user.Name.Contains(BotNamePart) && (!user.IsFinished) && (!isUserNeedCard)))
                {
                    user.IsFinished = true;
                    await _userRepository.Update(user);
                }
            }

            await _cardRepository.Add(cardsToAdd);
            await UpdateUsersPoints(usersToUpdatePoints,lastMatch.Game.Id);
            await UpdateGameStatus(userName);
            await UpdateUsersStatus(userName);

            if (!isUserNeedCard && lastMatch.Game.Status != GameStatus.Finished)
            {
                await NextRound(userName, false);
            }
            return await GetLastMatch(userName);
        }

        private async Task<Match> GetLastMatchForNextRound(string userName)
        {
            var game = await _gameRepository.GetLastGame(userName);
            var gamers = await _userRepository.FindByGameId(game.Id);
            var cards = await _cardRepository.FindByGameId(game.Id);
            var match = new Match() { Game = game, Gamers = gamers, Rounds = cards };
            return match;
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
            List<GameRound> cards = new List<GameRound>();
            string userId = await _userRepository.GetUserId(userName);
            List<string> botsIds = (await _userRepository.GetBotsIds()).ToList();
            if (botsIds==null||botsIds.Count==0)
            {
                throw new Exception("List of bots ids is empty");
            }
            gamers.Add(new UserInGame() { GameId = newGameGuid, Name = userName, IsDealer = false, IsFinished = false, GamerStatus = GamerStatus.InGame, Points = 0, UserId = userId });
            gamers.Add(new UserInGame() { GameId = newGameGuid, Name = BotDealerNamePart, IsDealer = true, IsFinished = false, GamerStatus = GamerStatus.InGame, Points = 0, UserId = botsIds[botsIds.Count-1] });
            for (var i = 0; i < countOfBots-1; i++)
            {
                gamers.Add(new UserInGame() { GameId = newGameGuid, Name = BotNamePart + (i+1), IsDealer = false, IsFinished = false, GamerStatus = GamerStatus.InGame, Points = 0, UserId = botsIds[i] });
            }
            await _userRepository.Add(gamers);
            return gamers;
        }

        private async Task DealTwoStartCards(Guid gameId)
        {
            List<Card> usedCards = new List<Card>();
            List<GameRound> cardsToAdd = new List<GameRound>();
            var gamers = await _userRepository.FindByGameId(gameId);
            foreach (var gamer in gamers)
            {
                for (var i = 0; i < CountOfStartCards; i++)
                {
                    var newUsedCard = DealCardFromDeck(ref usedCards);
                    cardsToAdd.Add(new GameRound() { UserInGameId = gamer.Id, GameId = gamer.GameId, Points = newUsedCard.Points, Suit = newUsedCard.Suit.ToString(), Value = newUsedCard.Value.ToString(), RoundNumber = 0 });
                }
            }
            await _cardRepository.Add(cardsToAdd);
            await UpdateUsersPoints(gamers,gameId);
        }

        private Card DealCardFromDeck(ref List<Card> usedCards)
        {
            CardDeck deck = new CardDeck(true);
            return deck.DealCard(ref usedCards);
        }

        private async Task UpdateUsersPoints(IEnumerable<UserInGame> gamers,Guid gameId)
        {
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

        private List<Card> MapRaundToCard(IEnumerable<GameRound> raunds)
        {
            var cardList = new List<Card>();

            foreach (var raund in raunds)
            {
                cardList.Add(new Card(raund.Value, raund.Suit));
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
            var maxGamerPoints = gamers.Select(data => data.Points).Where(data => data <= PointsToNotLose).Max();
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








