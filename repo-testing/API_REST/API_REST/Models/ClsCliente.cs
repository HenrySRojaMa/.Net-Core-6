using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_REST.Models
{
    public class ClsCliente
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public int Edad { get; set; }
        public char EstadoCivil { get; set; }
    }
}