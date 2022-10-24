using System;
using System.Collections.Generic;

namespace Taller1.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cabeceras = new HashSet<Cabecera>();
        }

        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Direccion2 { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public virtual ICollection<Cabecera> Cabeceras { get; set; }
    }
}
