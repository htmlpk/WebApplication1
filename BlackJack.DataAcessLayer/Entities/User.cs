﻿using Microsoft.AspNetCore.Identity;

namespace BlackJack.DataAccessLayer.Entities
{
    public class User:IdentityUser
    {
        public virtual UserInGame UserInGame { get; set; }
    }
}
