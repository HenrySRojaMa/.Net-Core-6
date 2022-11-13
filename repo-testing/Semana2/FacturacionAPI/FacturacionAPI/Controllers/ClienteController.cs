using FacturacionAPI.Logica;
using FacturacionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturacionAPI.Controllers
{
    [RoutePrefix("cliente")]
    public class ClienteController : ApiController
    {
        [Route("buscar")]
        [HttpGet]
        public Cliente Buscar(string Id)
        {
            SpCliente sp = new SpCliente();
            return sp.Buscar(Id);
        }
    }
}
