using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class LoginInput
    {
        [Required]
        [Display(Name ="Email Address")]
        public string  Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string  Password { get; set; }
        
        [Required]
        [Display(Name = "Remember me")]
        public bool IsRemember { get; set; }
    }
}
