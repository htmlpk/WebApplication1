using BlackJack.BusinessLogicLayer;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.UI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IGameService _gameService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IGameService gameService, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gameService = gameService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNames()
        {
            try
            {
                var userNames = _userManager.Users.Where(x => !x.Email.Contains("Bot")).Select(y => y.Email);
                return Ok(userNames);
            }
            catch (Exception e)
            {
                return BadRequest(RequestTypes.Error);
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> LogIn(string userName)
        {
            try
            {
                var loginService = new LoginHelper(_userManager, _signInManager);
                var token = await loginService.Login(userName);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(RequestTypes.LoginError);
            }
        }
    }
}
