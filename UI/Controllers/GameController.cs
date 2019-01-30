using BlackJack.BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [Authorize]
        [HttpGet("{user}")]
        public async Task<IActionResult> GetLastMatch(string user)
        {
            try
            {
                var lastMatch =  await _gameService.GetLastMatch(User.Identity.Name);
                return Ok(lastMatch);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> NextRound([FromBody] bool isCardNeed, string userName = null)
        {
            try
            {
                var result = await _gameService.NextRound(User.Identity.Name, isCardNeed);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
