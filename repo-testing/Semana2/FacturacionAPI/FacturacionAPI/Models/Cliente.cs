using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionAPI.Models
{
    public class Cliente
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Foto { get; set; }
    }
}