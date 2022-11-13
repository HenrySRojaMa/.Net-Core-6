using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionAPI.Models
{
    public class ClsProducto
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }

    }
}