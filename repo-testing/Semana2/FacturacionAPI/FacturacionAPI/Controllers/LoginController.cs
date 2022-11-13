using FacturacionAPI.ClienteServicio;
using FacturacionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturacionAPI.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("autenticar")]
        public ClsTransaccionAPI login(Usuario objUsusario) {
            ClsTransaccionAPI rpta = new ClsTransaccionAPI();

            try
            {
                Autorizacion servicio = new Autorizacion();

                rpta = servicio.autorizar(objUsusario); 
            }
            catch (Exception ex) {
                rpta.Codigo = "999";
                rpta.Mensaje = "Ocurrio un error " + ex.Message.ToString(); 
           
            }
            return rpta;
        }
    }
}
