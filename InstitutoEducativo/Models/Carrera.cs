using System;

public class Carrera
{
	public Guid CarreraId { get; set; }

	public string Nombre { get; set; }

	public ICollection <Materia> Materias { get; set; }

	public ICollection <Alumno> Alumnos { get; set; }


	public Carrera()
	{
	}
}
