using System;
using System.Collections.Generic;


public class Materia
{
	public Materia()
	{
	public Guid MateriaId { get; set; }

	public string CodigoMateria { get; set; }

	public string Nombre { get; set; }

	public string Descripcion { get; set; }

	public int CupoMaximo { get; set; }

	public ICollection <MateriaCursada> MateriasCursadas { get; set; }

	public ICollection <Calificacion> Calificaciones { get; set; }

	public Carrera carrera { get; set; }

}
}
