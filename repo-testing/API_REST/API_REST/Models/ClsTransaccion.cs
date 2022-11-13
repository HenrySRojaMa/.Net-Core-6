using System;

namespace API_REST.Models
{
    public class ClsTransaccion
    {
        public string Codigo { get; set; } //='000' OK  ;  '999' ERROR
        public string Mensaje { get; set; }
    }
}