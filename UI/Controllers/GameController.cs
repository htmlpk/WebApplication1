using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlackJack.BusinessLogicLayer;
using UI.Data.GameRepository;

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
                return await _gameServise.GetLastMatch(username); ;
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
                return await _gameServise.NextRound(username, isCardNeed); ;
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
