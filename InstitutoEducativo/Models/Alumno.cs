using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Alumno : Persona
	{

		public Guid AlumnoId { get; set; }

		public DateTime FechaAlta { get; set; }

		public Boolean Activo { get; set; }

		public int NumeroMatricula { get; set; }

		public List<AlumnoMateriaCursada> AlumnoMateriaCursada {get;set;}

		public Carrera Carrera { get; set; }

		public List<Calificacion> Calificaciones { get; set; }


		public Alumno()
		{
		}

        
    }
}