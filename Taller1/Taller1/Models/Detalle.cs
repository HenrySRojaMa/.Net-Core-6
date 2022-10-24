using System;
using System.Collections.Generic;

namespace Taller1.Models
{
    public partial class Detalle
    {
        public int Codigo { get; set; }
        public int NumeroCompra { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public virtual Cabecera NumeroCompraNavigation { get; set; } = null!;
        public virtual Producto ProductoNavigation { get; set; } = null!;
    }
}
