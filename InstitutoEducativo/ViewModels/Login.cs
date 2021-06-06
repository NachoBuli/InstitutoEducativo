using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = Validaciones.Required)]
        [EmailAddress(ErrorMessage = Validaciones.Required)]
        public string Email { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool Recordarme { get; set; }
    }
}
