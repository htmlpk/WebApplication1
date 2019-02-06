using BlackJack.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class HistoryController : BaseController
    {
        private IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
                var allGames = await _historyService.GetAll(User.Identity.Name);
                return Ok(allGames);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchById(Guid id)
        {
                var match = await _historyService.GetMatchById(id);
                return Ok(match);
        }
    }
}