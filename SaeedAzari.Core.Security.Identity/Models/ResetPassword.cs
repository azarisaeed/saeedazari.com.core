using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedAzari.Core.Security.Identity.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
