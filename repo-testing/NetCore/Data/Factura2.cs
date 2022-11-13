using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Factura2 : Factura
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Detalle2> Detalle { get; set; }
    }
}
