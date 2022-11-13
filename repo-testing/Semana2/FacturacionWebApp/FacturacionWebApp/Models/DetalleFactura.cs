using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionWebApp.Models
{
    public class DetalleFactura
    {
        public int idDetalle { get; set; }
        public int idCodigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionProducto { get; set;  }
        public double precioProducto { get; set; }
        public int cantidadProducto { get; set; }
        public double subtotal { get; set; }
        public int stockActualizado { get; set; }
    }
}