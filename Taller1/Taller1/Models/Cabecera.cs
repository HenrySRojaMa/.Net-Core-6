using System;
using System.Collections.Generic;

namespace Taller1.Models
{
    public partial class Cabecera
    {
        public Cabecera()
        {
            Detalles = new HashSet<Detalle>();
        }

        public int NumeroCompra { get; set; }
        public string Cliente { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int FormaPago { get; set; }
        public int Estatus { get; set; }
        public int DireccionEntrega { get; set; }

        public virtual Cliente ClienteNavigation { get; set; } = null!;
        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}
