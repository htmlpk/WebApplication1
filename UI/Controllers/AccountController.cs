using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UI.Data;
using System.Web;
using UI.Data.GameRepository;
using UI.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using BlackJack.BusinessLogicLayer;
using Microsoft.IdentityModel.Tokens;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        

        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IGameService _gameService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IGameService gameService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _gameService = gameService;
        }

        [HttpGet("{username}")]
        public async Task<IEnumerable<Game>> Get(string username)
        {
            await Login(username);
            var jwtUsername = GetIdentity(username, username).Name;
            return await _gameService.GetAll(jwtUsername);
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
                var jwtUsername = GetIdentity(username, username).Name;
                await _gameService.StartGame(jwtUsername, countofbots);
            }
            catch (Exception e)
            {

                throw;
            }
            return Ok();
        }




        public async Task<IActionResult> Register(string username)
        {
            User user = new User { Email = username, UserName = username };
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username)
        {

            var result = await _signInManager.PasswordSignInAsync(username, username, false, false);

            if (!result.Succeeded)
            {
                await Register(username);

            };

            try
            {
                await Authenticate(username);
            }
            catch (Exception e)
            {

                throw;
            }
            

            var a = User.Identity.Name;

            return Ok();
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User person = _userManager.Users.FirstOrDefault(x => x.Email == username);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
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
