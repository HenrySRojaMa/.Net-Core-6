using System;
using System.Collections.Generic;

namespace Repository_UnitOfWork.Models
{
    public partial class Cliente:BaseEntity
    {
        public Cliente()
        {
            Cabeceras = new HashSet<Cabecera>();
        }

        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Direccion2 { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public virtual ICollection<Cabecera> Cabeceras { get; set; }
    }
}
