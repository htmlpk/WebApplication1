using BlackJack.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlackJack.UI.Helpers
{
    public class LoginHelper
    {
        private readonly AppSettings _appSettings;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public LoginHelper(UserManager<User> userManager, SignInManager<User> signInManager, AppSettings appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings;
        }

        public async Task Register(string userName)
        {
            User user = new User { Email = userName, UserName = userName };
            var result = await _userManager.CreateAsync(user, userName);
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
        }

        public async Task<string> Login(string userName)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, userName, false, false);
            if (!result.Succeeded)
            {
                await Register(userName);
            };
            User user = _userManager.Users.FirstOrDefault(x => x.Email == userName);
            var token = GetToken(userName);
            return token;
        }

        private string GetToken(string userName)
        {
            var identity = GetIdentity(userName);
            if (identity == null)
            {
                return null;
            }
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = encodedJwt
            };
            var stringToken = JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return stringToken;
        }

        private ClaimsIdentity GetIdentity(string userName)
        {
            User user = _userManager.Users.FirstOrDefault(x => x.Email == userName);
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
