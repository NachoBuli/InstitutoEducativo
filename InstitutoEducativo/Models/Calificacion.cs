
using System;

namespace InstitutoEducativo.Models
{
    public class Calificacion
	{
		public Guid CalificacionId { get; set; }

		public int NotaFinal { get; set; }

		public Materia Materia { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

		public Profesor Profesor { get; set; }

		public Alumno Alumno { get; set; }

		public Calificacion()
		{
		}

	}
}