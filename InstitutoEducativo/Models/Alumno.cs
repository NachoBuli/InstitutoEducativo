using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Alumno : Persona
	{

		private Guid AlumnoId { get; set; }

		private DateTime FechaAlta { get; set; }

		private Boolean Activo { get; set; }

		private int NumeroMatricula { get; set; }

		private List<AlumnoMateriaCursada> AlumnoMateriaCursada {get;set;}

		private Carrera Carrera { get; set; }

		private List<Calificacion> Calificaciones { get; set; }


		public Alumno()
		{
		}

        
    }
}