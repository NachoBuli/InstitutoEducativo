using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class MateriaCursada
	{
		public Guid MateriaCursadaId { get; set; }

		public string Nombre { get; set; }

		public int Anio { get; set; }

		public int Cuatrimestre { get; set; }

		public Boolean Activo { get; set; }

		public Materia Materia { get; set; }

		public Profesor Profesor { get; set; }

		public List<AlumnoMateriaCursada> AlumnoMateriaCursadas { get; set; }

	}
}
