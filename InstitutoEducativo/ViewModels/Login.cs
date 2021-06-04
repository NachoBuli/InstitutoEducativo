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
        [Required(ErrorMessage = Validaciones._required)]
        [EmailAddress(ErrorMessage = Validaciones._required)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool Recordarme { get; set; }
    }
}
