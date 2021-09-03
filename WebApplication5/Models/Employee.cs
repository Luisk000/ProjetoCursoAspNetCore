using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é necessário"), MaxLength(50, ErrorMessage = "O nome não pode possuir mais de 50 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "É necessário selecionar um departamento")]
        [Display(Name = "Departamento")]
        public Dept? Department { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Formato de email inválido")]
        [Required(ErrorMessage = "O email é necessário")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
    }
}
