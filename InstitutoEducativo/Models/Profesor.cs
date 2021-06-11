using InstitutoEducativo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{ 
public class Profesor : Empleado // Antes de realizar la migracion heredaba de Empleado 

{
        //[Required(ErrorMessage = Validaciones.Required)]
        //public string Legajo { get; set; }
        public ICollection<MateriaCursada> MateriasCursadasActivas { get; set; }
        public ICollection<Calificacion> CalificacionesRealizadas { get; set; }

	
}
}