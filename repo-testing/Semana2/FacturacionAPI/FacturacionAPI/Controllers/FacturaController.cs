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
    [RoutePrefix("factura")]
    public class FacturaController : ApiController
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
