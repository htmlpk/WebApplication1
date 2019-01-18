using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Data;
using UI.Data.GameRepository;

using UI.Entities;
using BlackJack.BusinessLogicLayer;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        
        private IGameService _gameServise;

        public GameController(IGameService gameService)
        {
            _gameServise = gameService;

        }

       
        [HttpGet("{username}")]
        public async Task<Match> Get(string username)
        {
            try
            {
                var game = await _gameServise.GetLastMatch(username);
                return game;
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

        [HttpPut("{username}")]
        public async Task<Match> Put(string username, [FromBody] bool isCardNeed)
        {
            try
            {
                var a = await _gameServise.NextRound(username, isCardNeed);
                return a;
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

      
        [HttpDelete("{id}")]
        public async Task<Match> Delete(Guid id)
        {
            var match = await _gameServise.GetMatchById(id);
            return match;
        }


    }
}
