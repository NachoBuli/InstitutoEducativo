using InstitutoEducativo.Data;
using InstitutoEducativo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstitutoEducativo.Models
{
	public class Usuario
	{
		[Key]
		public Guid UsuarioId { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[MaxLength(ErrorMessage = Validaciones._maxLength)]
		public string Nombre { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime FechaAlta { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[ForeignKey(nameof(Persona))]
		public Persona Persona { get; set; }


	}

}
