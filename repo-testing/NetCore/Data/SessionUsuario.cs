using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class SessionUsuario
    {
        public Cliente cliente { get; set; }
        public List<DetalleFactura> detalleFactura { get; set; }
        public double Total { get; set; }
        public double Descuento { get; set; }
        public double IVA { get; set; }
        public double TotalaPagar { get; set; }
        public string Token { get; set; }

        public SessionUsuario()
        {
            detalleFactura = new List<DetalleFactura>();
        }
    }
}
