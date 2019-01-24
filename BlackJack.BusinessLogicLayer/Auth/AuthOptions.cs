﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlackJack.BusinessLogicLayer
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "http://localhost:4200/"; 
        const string KEY = "mysupersecret_secretkey!123";  
        public const int LIFETIME = 30; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
