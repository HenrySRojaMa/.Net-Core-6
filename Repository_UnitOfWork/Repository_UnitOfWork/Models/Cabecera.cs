using System;
using System.Collections.Generic;

namespace Repository_UnitOfWork.Models
{
    public partial class Cabecera:BaseEntity
    {
        public Cabecera()
        {
            Detalles = new HashSet<Detalle>();
        }

        public int Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int FormaPago { get; set; }
        public int Estatus { get; set; }
        public int DireccionEntrega { get; set; }

        public virtual Cliente ClienteNavigation { get; set; } = null!;
        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}
