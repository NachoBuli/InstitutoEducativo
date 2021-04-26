using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Alumno : Persona
	{


		public Boolean Activo { get; set; }

		public int NumeroMatricula { get; set; }

		public List<AlumnoMateriaCursada> AlumnosMateriasCursadas {get;set;}

		public Carrera Carrera { get; set; }


		public Alumno()
		{
		}

        
    }
}