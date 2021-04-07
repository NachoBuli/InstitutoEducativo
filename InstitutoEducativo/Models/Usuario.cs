using System;

public abstract class Usuario
{
	public  Usuario()
	{	
	}

	public Guid UsuarioId { get; set; }

	public string NombreUsu { get; set; }

	public string Nombre { get; set; }

	public string Apellido { get; set; }

	public string Email { get; set; }

	public DateTime FechaAlta { get; set; }

	public string Password { get; set; }

	public string Telefono { get; set; }

	public string Direccion { get; set; }

	public string Dni { get; set; }


}
