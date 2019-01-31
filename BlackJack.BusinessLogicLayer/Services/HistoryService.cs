using BlackJack.DataAccessLayer.Entities;
using BlackJack.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogicLayer.Services
{
    class HistoryService : IHistoryService
    {
        private IGameRepository _gameRepository;
        private IUserRepository _userRepository;
        private ICardRepository _cardRepository;

        public HistoryService(IGameRepository gameRepository, IUserRepository userRepository, ICardRepository cardRepository)
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
