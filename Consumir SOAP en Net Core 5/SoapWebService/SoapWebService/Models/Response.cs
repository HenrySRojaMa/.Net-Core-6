using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoapWebService.Models
{
    public class Response
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public object obj { get; set; }
    }
}