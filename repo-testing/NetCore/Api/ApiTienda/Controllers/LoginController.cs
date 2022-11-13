using Data;
using Logica;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("autenticar")]
        public ClsTransaccionAPI login(Usuario objUsusario)
        {
            ClsTransaccionAPI rpta = new ClsTransaccionAPI();

            try
            {
                Autorizacion servicio = new Autorizacion();

                rpta = servicio.autorizar(objUsusario);
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = "Ocurrio un error " + ex.Message.ToString();

            }
            return rpta;
        }

    }
}
