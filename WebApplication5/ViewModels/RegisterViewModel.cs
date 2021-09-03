using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Utilities;

namespace WebApplication5.ViewModels
{
    public class RegisterViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Formato de email inválido")]
        [Required(ErrorMessage = "Defina um email válido")]
        [EmailAddress]
        [Remote(action: "isEmailInUse", controller: "Account")]
        // [ValidEmailDomain(allowedDomain: "senhorCaixa.com", ErrorMessage = "Domínio do email deve ser senhorCaixa.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Defina uma senha"), MinLength(6, ErrorMessage = "A senha não pode possuir menos de 6 caracteres"), MaxLength(15, ErrorMessage = "A senha não pode possuir mais de 15 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password",
            ErrorMessage = "As senhas providenciadas são diferentes")]
        public string ConfirmPassword { get; set; }
    }
}
