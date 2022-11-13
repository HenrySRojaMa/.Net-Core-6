using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioAutenticacion.Models
{
    public class ClsTransaccion
    {
        public string Codigo { get; set; } //='000' OK  ;  '999' ERROR
        public string Mensaje { get; set; }
    }
}