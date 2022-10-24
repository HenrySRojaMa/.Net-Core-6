using System;
using System.Collections.Generic;

namespace Taller1.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Detalles = new HashSet<Detalle>();
        }

        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}
