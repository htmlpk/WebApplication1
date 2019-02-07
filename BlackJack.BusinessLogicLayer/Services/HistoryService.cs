using AutoMapper;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.DataAccessLayer.Repository;
using BlackJack.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer.Services
{
    public class HistoryService : IHistoryService
    {
        private IMapper _mapper;
        private IGameRepository _gameRepository;
        private IUserRepository _userRepository;
        private ICardRepository _cardRepository;

        public HistoryService(IGameRepository gameRepository, IUserRepository userRepository, ICardRepository cardRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Game>> GetAll(string userName)
        {
            var games = await _gameRepository.GetAll(userName);
            return games;
        }

        public async Task<MatchViewModel> GetMatchById(Guid id)
        {
            var game = await _gameRepository.FindById(id.ToString());
            var gamers = await _userRepository.FindByGameId(game.Id);
            var cards = await _cardRepository.FindByGameId(game.Id);
            var match = new MatchViewModel() { Game = _mapper.Map(game,new GameViewModel()) , Gamers = _mapper.Map(gamers, new List<UserInGameViewModel>()), Rounds = _mapper.Map(cards, new List<RoundViewModel>()) };
            return match;
        }
    }
}
