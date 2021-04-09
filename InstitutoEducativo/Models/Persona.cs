using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
    public class Persona
    {

        public Guid PersonaId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public string Dni { get; set; }

        public Persona() { }

        
    }

    

}
