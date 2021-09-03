using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "O nome é necessário"), MaxLength(50, ErrorMessage = "O nome não pode possuir mais de 50 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "É necessário selecionar um departamento")]
        [Display(Name = "Departamento")]
        public Dept? Department { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Formato de email inválido")]
        [Required(ErrorMessage = "O email é necessário")]
        public string Email { get; set; }
       [Display(Name = "Selecione uma foto (opcional)")]
        public IFormFile Photo { get; set; }
    }
}
