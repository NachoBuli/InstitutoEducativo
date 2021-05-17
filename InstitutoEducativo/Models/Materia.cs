using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
	public class Materia
	{

		[Key]
		public Guid MateriaId { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[MaxLength(100, ErrorMessage = Validaciones._maxLength)]
		public string Nombre { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[MaxLength(6, ErrorMessage = Validaciones._maxLength)]
		public string CodigoMateria { get; set; }

        [Required(ErrorMessage = Validaciones._required)]
		[MaxLength(150, ErrorMessage = Validaciones._maxLength)]
		public string Descripcion { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[Range(0, 1000, ErrorMessage = "Ingresa un numero mayor a cero y menor a 1000")]
		public int CupoMaximo { get; set; }

		public ICollection<MateriaCursada> MateriasCursadas { get; set; }

		public ICollection<Calificacion> Calificaciones { get; set; }

		[ForeignKey(nameof(Carrera))]
        public Guid CarreraId { get; set; }
        public Carrera Carrera { get; set; }
	}
}