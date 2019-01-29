using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlackJack.UI
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer";
        public const string Audience = "http://localhost:4200/"; 
        public const string Key = "mysupersecret_secretkey!123";  
        public const int Lifetime = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
