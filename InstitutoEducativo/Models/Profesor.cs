using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{ 
public class Profesor : Empleado

{
	
	public ICollection<MateriaCursada> MateriasCursadasActivas { get; set; }

	public ICollection<Calificacion> CalificacionesRealizadas { get; set; }

	
}
}