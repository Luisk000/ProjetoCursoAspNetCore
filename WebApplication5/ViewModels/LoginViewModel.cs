using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email não encontrado")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha incorreta")]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name="Remember me")]
        public bool RememberMe { get; set; }
    }
}
