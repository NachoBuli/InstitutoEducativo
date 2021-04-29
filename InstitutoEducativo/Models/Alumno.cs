using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class Alumno : Persona
	{


		public bool Activo { get; set; } // supongo que no resistriciones seran necesarios
		public int NumeroMatricula { get; set; }
		public List<AlumnoMateriaCursada> AlumnosMateriasCursadas {get;set;}
		public Carrera Carrera { get; set; }


		

        
    }
}