using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class MateriaCursada
	{
		private Guid MateriaCursadaId { get; set; }

		private String Nombre { get; set; }

		private int Anio { get; set; }

		private int cuatrimestre { get; set; }

		private Boolean Activo { get; set; }

		private Materia Materia { get; set; }

		private Profesor Profesor { get; set; }

		private List<AlumnoMateriaCursada> AlumnoMateriaCursadas { get; set; }

		private List<Calificacion> Calificaciones { get; set; }

		public MateriaCursada()
		{
		}
	}
}
