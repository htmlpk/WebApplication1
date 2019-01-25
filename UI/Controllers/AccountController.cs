using BlackJack.BusinessLogicLayer;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.UI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.UI.Controllers
{
    [Route("api/[controller]")]
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
        public IQueryable<string> Get()
        {
            return _userManager.Users.Where(item => !item.Email.Contains("Bot")).Select(item2 => item2.Email);
        }

        
        [HttpGet("{username}")]
        public async Task<string> Get(string username)
        {
            var userToken = await Login(username);
            return userToken;
        }
        
        [HttpPut("{username}")]
        public async Task Put(string username)
        {
            var countofbots = "";
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    countofbots = await reader.ReadToEndAsync();
                }
                await _gameService.StartGame(username, int.Parse(countofbots));
            }
            catch (Exception e)
            {
                throw;
            }
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
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return Ok();
        }
        
        public async Task<string> Login(string username)
        {
            var result = await _signInManager.PasswordSignInAsync(username, username, false, false);
            if (!result.Succeeded)
            {
                await Register(username);
            };
            User user = _userManager.Users.FirstOrDefault(x => x.Email == username);
            var tokenFactory = new TokenFactory();
            return await GetToken(username);
        }

        private async Task<string> GetToken(string userName)
        {           
            var identity = GetIdentity(userName);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return null;
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt
            };
            Response.ContentType = "application/json";
            var a = JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
        private ClaimsIdentity GetIdentity(string username)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.Email == username);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
