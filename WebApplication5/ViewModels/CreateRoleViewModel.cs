﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Defina uma papel válida")]
        public string RoleName { get; set; }
    }
}
