using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
    public abstract class Persona
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [MaxLength(50, ErrorMessage = Validaciones.MaxLength)]
        public string UserName { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [MaxLength(50, ErrorMessage = Validaciones.MaxLength)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [MaxLength(50, ErrorMessage = Validaciones.MaxLength)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = Validaciones.Dni)]
        public string Dni { get; set; }

        [Required(ErrorMessage = Validaciones.Required)]
        [EmailAddress(ErrorMessage = Validaciones.Email)]
        public string Email { get; set; }

        [RegularExpression(@"[0-9]{10}", ErrorMessage = Validaciones.Telefono)]
        public string Telefono { get; set; }

        [MaxLength(100, ErrorMessage = Validaciones.MaxLength)]
        public string Direccion { get; set; }

        public string Legajo { get; set; }



    }

    

}
