using System;
using System.Collections.Generic;

namespace Repository_UnitOfWork.Models
{
    public partial class Producto:BaseEntity
    {
        public Producto()
        {
            Detalles = new HashSet<Detalle>();
        }

        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}
