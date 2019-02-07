using BlackJack.BusinessLogicLayer;
using BlackJack.UI.Helpers;
using BlackJack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class GameController : BaseController
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> StartGame([FromBody]StartGameViewModel model)
        {
                await _gameService.StartGame(model);
                return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLastMatch()
        {
                var lastMatch =  await _gameService.GetLastMatch(User.Identity.Name);
                return Ok(lastMatch);
        }

        [HttpPut]
        public async Task<IActionResult> NextRound([FromBody] bool isCardNeed)
        {
                var result = await _gameService.NextRound(User.Identity.Name, isCardNeed);
                return Ok(result);
        }
    }
}
