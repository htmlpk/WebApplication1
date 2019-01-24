using BlackJack.BusinessLogicLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Data.GameRepository;
using UI.Entities;

namespace BlackJack.UI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : Controller
    {
        private IGameService _gameService;

        public HistoryController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> Get()
        {
            var jwtUsername = User.Identity.Name;
            return await _gameService.GetAll(jwtUsername);
        }

        [HttpGet("{id}")]
        public async Task<Match> Get(Guid id)
        {
            var match = await _gameService.GetMatchById(id);
            return match;
        }
    }
}