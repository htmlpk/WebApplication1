using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Entities;

namespace UI.Data
{
    public class User:IdentityUser
    {
        public virtual UserInGame UserInGame { get; set; }
    }
}
