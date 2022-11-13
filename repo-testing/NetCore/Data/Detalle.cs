using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Detalle
    {
        public int CodigoFactura { get; set; }
        public int CodigoProducto { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
    }
}
