using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Your name")]
        
        public string Email { get; set; }
    }
}
