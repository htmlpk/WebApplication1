using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UI.Data;
using System.Web;
using UI.Data.GameRepository;
using UI.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IGameService _gameServise;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IGameService gameServise)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _gameServise = gameServise;
        }


        [HttpGet("{username}")]
        public async Task<List<Game>> Get(string username)
        {
            return await _gameServise.GetAll(username);

        }


        [HttpGet]
        public IQueryable<string> Get()
        {
            return _userManager.Users.Where(item => !item.Email.Contains("Bot")).Select(item2 => item2.Email);
        }


        [HttpPut("{username}")]
        public async Task<IActionResult> Put(string username, [FromBody]int countofbots)
        {
            await Login(username);
            try
            {
                await _gameServise.StartGame(username, countofbots);
            }
            catch (Exception e)
            {

                throw;
            }
            
            return Ok();
        }

        


        public async Task<IActionResult> Register(string username)
        {


            User user = new User { Email = username, UserName = username};
            
            var result = await _userManager.CreateAsync(user, username);

                       
            if (result.Succeeded)
            {
                try
                {
                    await _signInManager.SignInAsync(user, false);
                    var name = HttpContext.User.Identity.Name;
                    var a = _signInManager.Context.User.Identity.Name;
                }
                catch (Exception e)
                {

                    throw;
                }


            }
            return Ok();
        }

        public async Task<IActionResult> Login(string username)
        {

            await LogOff();
            var result = await _signInManager.PasswordSignInAsync(username, username, false, false);
            

            
            var a = _signInManager.Context.User.Identity.Name;
            var b = HttpContext.User.Identity.Name;

            if (!result.Succeeded)
            {
                await Register(username);

            };

            return Ok();
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  
            return Ok();
        }

    }





}
