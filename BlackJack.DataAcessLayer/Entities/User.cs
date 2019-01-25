using Microsoft.AspNetCore.Identity;

namespace BlackJack.DataAcessLayer.Entities
{
    public class User:IdentityUser
    {
        public virtual UserInGame UserInGame { get; set; }
    }
}
