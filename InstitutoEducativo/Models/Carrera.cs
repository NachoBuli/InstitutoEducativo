using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Carrera
	{
		[Key]
		public Guid CarreraId { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[MaxLength(50, ErrorMessage = Validaciones._maxLength)]
		public string Nombre { get; set; }

		public ICollection<Materia> Materias { get; set; }

		public ICollection<Alumno> Alumnos { get; set; }
	}
}