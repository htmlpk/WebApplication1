using BlackJack.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoryController : Controller
    {
        private IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var allGames = await _historyService.GetAll(User.Identity.Name);
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
                var match = await _historyService.GetMatchById(id);
                return Ok(match);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}