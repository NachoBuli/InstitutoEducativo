using System;

public class Profesor : Usuario
{
	public string Legajo { get; set; }

	public ICollection <MateriaCursada> MateriasCursadasActivas { get; set; }
	
	public ICollection <Calificacion> CalificacionesRealizadas { get; set; }

	public Profesor()
	{

	}
}
