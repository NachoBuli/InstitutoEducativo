

namespace InstitutoEducativo.Models
{
    public class Calificacion
	{
		private int NotaFinal { get; set; }

		private Materia Materia { get; set; }

        private MateriaCursada materiaCursada { get; set; }

		private Profesor profesor { get; set; }

		private Alumno alumno { get; set; }

		public Calificacion()
		{
		}

	}
}