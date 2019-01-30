using BlackJack.BusinessLogicLayer;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoryController : Controller
    {
        private IGameService _gameService;

        public HistoryController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var allGames = await _gameService.GetAll(User.Identity.Name);
                return Ok(allGames);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchById(Guid id)
        {
            try
            {
                var match = await _gameService.GetMatchById(id);
                return Ok(match);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}