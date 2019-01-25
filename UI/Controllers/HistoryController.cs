using BlackJack.BusinessLogicLayer;
using BlackJack.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            try
            {
                var jwtUsername = User.Identity.Name;
                var a = await _gameService.GetAll(jwtUsername);
                return await _gameService.GetAll(jwtUsername);
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

        [HttpGet("{id}")]
        public async Task<Match> Get(Guid id)
        {
            var match = await _gameService.GetMatchById(id);
            return match;
        }
    }
}