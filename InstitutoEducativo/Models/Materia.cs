using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
	public class Materia
	{
		public Materia()
		{
		}

		private Guid MateriaId { get; set; }

		private string CodigoMateria { get; set; }

		private string Nombre { get; set; }

		private string Descripcion { get; set; }

		private int CupoMaximo { get; set; }

		private ICollection<MateriaCursada> MateriasCursadas { get; set; }

		private ICollection<Calificacion> Calificaciones { get; set; }

		public Carrera carrera { get; set; }
	}
}