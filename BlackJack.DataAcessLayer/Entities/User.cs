using Microsoft.AspNetCore.Identity;
using UI.Entities;

namespace UI.Data
{
    public class User:IdentityUser
    {
        public virtual UserInGame UserInGame { get; set; }
    }
}
