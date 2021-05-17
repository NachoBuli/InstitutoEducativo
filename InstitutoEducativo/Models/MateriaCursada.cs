﻿using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{

	public class MateriaCursada
	{
		public Guid MateriaCursadaId { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[MaxLength(100, ErrorMessage = Validaciones._maxLength)]
		public string Nombre { get; set; }
		
		[Required(ErrorMessage = Validaciones._required)]
		[Range(1, 6, ErrorMessage = "Ingresa un numero mayor a 1 y menor a 6")]
		public int Anio { get; set; }

		[Required(ErrorMessage = Validaciones._required)]
		[Range(1, 2, ErrorMessage = "Ingresa un numero mayor a 1 y menor a 2")]
		public int Cuatrimestre { get; set; }

		public bool Activo { get; set; }

		[ForeignKey(nameof(Materia))]
        public Guid MateriaId { get; set; }
        public Materia Materia { get; set; }

		[ForeignKey(nameof(Profesor))]
		public Guid ProfesorId { get; set; }
		public Profesor Profesor { get; set; }

		public List<AlumnoMateriaCursada> AlumnoMateriaCursadas { get; set; }

		public ICollection<Calificacion> Calificaciones { get; set; }

	}
}
