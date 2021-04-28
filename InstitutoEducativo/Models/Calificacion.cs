using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
    public class Calificacion
	{
		public Guid CalificacionId { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		public int NotaFinal { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		public Materia Materia { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		public MateriaCursada MateriaCursada { get; set; }

		public Profesor Profesor { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		public Alumno Alumno { get; set; }

	

	}
}