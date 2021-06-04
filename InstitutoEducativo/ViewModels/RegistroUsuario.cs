using InstitutoEducativo.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.ViewModels
{
    public class RegistroUsuario
    {
        [Required (ErrorMessage = Validaciones._required)]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = Validaciones._required)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Ingresá tu DNI sin puntos. Ej.: 12345678")]
        public string Dni { get; set; }
        
        [Required(ErrorMessage = Validaciones._required)]
        [EmailAddress]
        [Remote(action: "EmailLibre", controller: "Accounts")]
        public string Email { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Ingresá tu número telefónico sin 0 ni 15. Ej.: Cód. Área: 11 y Número: 2345678")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
        public string Direccion { get; set; }

        [DataType(DataType.Password)]
        [MinLength (5, ErrorMessage = "Ingresa un minimo de 5 caracteres")]
        [Required(ErrorMessage = Validaciones._required)]
        public string Contrasena { get; set; }

        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Ingresa un minimo de 5 caracteres")]
        [Display(Name = "Confirmación de Password")]
        [Compare("Password", ErrorMessage = "La password de confirmación no es igual. Por favor, verifiquela.")]
        [Required(ErrorMessage = Validaciones._required)]
        public string ConfirmacionContrasena { get; set; }

       
    }
}
