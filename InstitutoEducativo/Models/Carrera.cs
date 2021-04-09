using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Carrera
	{
		private Guid CarreraId { get; set; }

		private string Nombre { get; set; }

		private ICollection<Materia> Materias { get; set; }

		private ICollection<Alumno> Alumnos { get; set; }


		public Carrera()
		{
		}
	}
}