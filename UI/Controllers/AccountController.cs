using BlackJack.BusinessLogicLayer;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.UI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly AppSettings _appSettings;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IGameService _gameService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IGameService gameService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNames()
        {
            var userNames = _userManager.Users.Where(x => !x.Email.Contains("Bot")).Select(y => y.Email);
            return Ok(userNames);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> LogIn(string userName)
        {
            var loginHelper = new LoginHelper(_userManager, _signInManager);
            var token = await loginHelper.Login(userName);
            return Ok(token);
        }
    }
}
