using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
    public class Persona
    {
        [Key]
        public Guid PersonaId { get; set; }

        [Required (ErrorMessage = Validaciones._required)]
        [MaxLength(50, ErrorMessage = Validaciones._maxLength)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
        [MaxLength(50, ErrorMessage = Validaciones._maxLength)]
        public string Apellido { get; set; }

        [ForeignKey(nameof(Usuario))]
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Ingresá tu DNI sin puntos. Ej.: 12345678")]
        public string Dni { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
        [EmailAddress(ErrorMessage = "Ingresá una dirección de correo electrónico válida")]
        public string Email { get; set; }

        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Ingresá tu número telefónico sin 0 ni 15. Ej.: Cód. Área: 11 y Número: 2345678")]
        public string Telefono { get; set; }

        [MaxLength(100, ErrorMessage = Validaciones._maxLength)]
        public string Direccion { get; set; }

       
     

        
    }

    

}
