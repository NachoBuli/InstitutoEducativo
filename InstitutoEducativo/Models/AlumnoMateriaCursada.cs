using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
	public class AlumnoMateriaCursada
	{
		private Guid PersonaId { get; set; }

		private Guid MateriaCursadaId { get; set; }

		private AlumnoMateriaCursada() { }

        public Persona Persona { get; set; }

        public MateriaCursada MateriaCursada { get; set; }
	}
	
}