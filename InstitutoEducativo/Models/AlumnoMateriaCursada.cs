using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
	public class AlumnoMateriaCursada
	{
		public Guid PersonaId { get; set; }

		public Guid MateriaCursadaId { get; set; }

		public AlumnoMateriaCursada() { }

        public Persona Persona { get; set; }

        public MateriaCursada MateriaCursada { get; set; }
	}
	
}