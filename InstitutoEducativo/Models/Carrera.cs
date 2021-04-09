using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Carrera
	{
		public Guid CarreraId { get; set; }

		public string Nombre { get; set; }

		public ICollection<Materia> Materias { get; set; }

		public ICollection<Alumno> Alumnos { get; set; }


		public Carrera()
		{
		}
	}
}