using Data;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ServiceReference1.LogginSoapClient;

namespace ApiTienda.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [Route("buscar")]
        [HttpGet]
        public Cliente Buscar(string Id)
        {
            SpCliente sp = new SpCliente();
            return sp.Buscar(Id);
        }
        [HttpPost]
        [Route("actualizar")]
        public ClsTransaccionAPI Actualizar(Cliente c)
        {
            var rpta = new SpCliente().ActualizarCliente(c);
            return rpta ;
        }
    }
}
