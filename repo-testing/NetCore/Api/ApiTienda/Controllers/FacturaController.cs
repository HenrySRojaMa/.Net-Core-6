using Data;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {

        [Route("ingresarcab")]
        [HttpPost]
        public ClsTransaccionAPI IngresarCabecera(Factura factura)
        {
            SpFactura sp = new SpFactura();
            return sp.IngresarCabecera(factura);
        }

        [Route("ingresardet")]
        [HttpPost]
        public ClsTransaccionAPI IngresarDetalle(List<Detalle> detalles)
        {
            SpFactura sp = new SpFactura();

            return sp.IngresarDetalles(detalles);
        }

        [Route("consultar")]
        [HttpGet]
        public Factura2 Consultar(int codigoFactura)
        {
            SpFactura sp = new SpFactura();
            return sp.Consultar(codigoFactura);
        }

    }
}
