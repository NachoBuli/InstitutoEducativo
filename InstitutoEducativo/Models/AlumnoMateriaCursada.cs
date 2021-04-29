using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
	public class AlumnoMateriaCursada
	{
        [Key,ForeignKey("Alumno")]
        public Guid AlumnoId { get; set; }

        
        [Key, ForeignKey("MateriaCursada")]
        public Guid MateriaCursadaId { get; set; }
        


        public Alumno Alumno { get; set; }

        public MateriaCursada MateriaCursada { get; set; }

        public Guid CalificacionId { get; set; }

        public Calificacion Calificacion { get; set; }

	}
	
}