using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionAPI.Models
{
    public class Factura2:Factura
    {

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Detalle2> Detalle { get; set;  }

     
    }
}