using System;
using System.Collections.Generic;

namespace Repository_UnitOfWork.Models
{
    public partial class Detalle:BaseEntity
    {
        public int NumeroCompra { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public virtual Cabecera NumeroCompraNavigation { get; set; } = null!;
        public virtual Producto ProductoNavigation { get; set; } = null!;
    }
}
