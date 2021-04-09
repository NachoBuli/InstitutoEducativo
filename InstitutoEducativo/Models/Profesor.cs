using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{ 
public class Profesor : Persona

{
	private string Legajo { get; set; }

	private ICollection<MateriaCursada> MateriasCursadasActivas { get; set; }

	private ICollection<Calificacion> CalificacionesRealizadas { get; set; }

	public Profesor()
	{

	}
}
}