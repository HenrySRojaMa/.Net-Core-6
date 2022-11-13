using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionAPI.Models
{
    public class Factura
    {
        public int Codigo { get; set; }
        public string Fecha { get; set; }
        public int CodigoCliente { get; set; }
        public double Total { get; set; }
        public double Descuento { get; set; }
        public double IVA { get; set; }
        public double TotalaPagar { get; set; }
    }
}