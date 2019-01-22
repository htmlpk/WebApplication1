using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlackJack.BusinessLogicLayer;
using UI.Data.GameRepository;
using Microsoft.AspNetCore.Authorization;

namespace UI.Controllers
{
    [Authorize]
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
               // var jwtUsername = GetIdentity(username, username).Name;
                return await _gameServise.GetLastMatch(username); ;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        [HttpPut("{username}")]
        public async Task<Match> Put([FromBody] bool isCardNeed, string username = null)
        {
            try
            {
                return await _gameServise.NextRound(username, isCardNeed);
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
