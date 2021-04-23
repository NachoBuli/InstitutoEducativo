using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
	public class AlumnoMateriaCursada
	{
		public Guid AlumnoId { get; set; }

		public Guid MateriaCursadaId { get; set; }

        public Alumno Alumno { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

		public Calificacion Calificacion { get; set; }

	}
	
}