using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Logica;
namespace ApiArchivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        [Route("listar")]
        [HttpGet]
        public List<Cliente> ListarClientes()
        {
            SpCliente sp = new SpCliente();
            return sp.ListarClientes();
        }
        [Route("filtrar")]
        [HttpPost]
        public List<Cliente> FiltarClientes(Cliente c)
        {
            return new SpCliente().FiltarClientes(c);
        }
        [HttpPost]
        [Route("actualizar")]
        public int Actualizar(Cliente c)
        {
            return new SpCliente().ActualizarClientes(c);
        }
    }
}
