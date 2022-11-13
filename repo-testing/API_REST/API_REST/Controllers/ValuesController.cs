using API_REST.Logica;
using API_REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_REST.Controllers
{
    [RoutePrefix("api/cliente")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("listar")]

        public List<ClsCliente> Get()
        {
            return SpCliente.Listar();
        }

        // GET api/values/5
        [Route("buscar")]
        public ClsCliente Get(string Identificacion)
        {
            return SpCliente.Buscar(Identificacion) ;
        }

        // POST api/values
        [HttpPost]
        [Route("ingresar")]
        public ClsTransaccion Post([FromBody] ClsCliente cliente)
        {
            return SpCliente.Insertar(cliente);
        }

        // PUT api/values/5
        [HttpPost]
        [Route("actualizar")]
        public ClsTransaccion Put(ClsCliente cliente)
        {
            return SpCliente.Actualizar(cliente);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        /*[HttpPost]
        [Route("ingresar")]
        public ClsTransaccion IngresarCliente(ClsCliente objCliente)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            
            rpta.Codigo = "000";
            rpta.Mensaje = "OK";

            return rpta; 

        }*/
    }
}
