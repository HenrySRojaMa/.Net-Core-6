using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionWebApp.Models
{
    public class SessionUsuario
    {
        public Cliente cliente { get; set; }
        public List<DetalleFactura> detalleFactura { get; set; }
        public double Total { get; set; }
        public double Descuento { get; set; }
        public double IVA { get; set; }
        public double TotalaPagar { get; set; }

        public SessionUsuario()
        {
            detalleFactura = new List<DetalleFactura>();
        }
    }
}