using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoapWebService.Models
{
    public class Persona
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
    }
}