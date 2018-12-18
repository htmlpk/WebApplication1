using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Your name")]
        public string Email { get; set; }

        
        public string ReturnUrl { get; set; }

        public string Password { get { return "9x7176785aaa"; } set {; } }
    }
}
