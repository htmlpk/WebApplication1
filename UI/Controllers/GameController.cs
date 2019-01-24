using BlackJack.BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;
using UI.Data.GameRepository;

namespace UI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Authorize]
        [HttpGet("{user}")]
        public async Task<Match> Get(string user)
        {
            try
            {
                return await _gameService.GetLastMatch(User.Identity.Name);
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
                return await _gameService.NextRound(User.Identity.Name, isCardNeed);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<Match> Delete(Guid id)
        {
            var match = await _gameService.GetMatchById(id);
            return match;
        }
    }
}
