using FacturacionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FacturacionAPI.Controllers
{
    [RoutePrefix("producto")]
    public class ProductoController : ApiController
    {
        //Producto_SP temp = new Producto_SP();

        [HttpPost]
        [Route("ingresar")]
        public ClsTransaccionAPI Ingresar([FromBody] ClsProducto Producto)
        {
            return Producto_SP.IngresarProducto(Producto);
        }

        [HttpPost]
        [Route("actualizar")]
        public ClsTransaccionAPI Actualizar(ClsProducto Producto)
        {            
            return Producto_SP.ActualizarProducto(Producto);
        }

        [HttpGet]
        [Route("listar")]
        public List<ClsProducto> Listar()
        {           
            return Producto_SP.ListarProducto();
        }

        [HttpGet]
        [Route("buscar")]
        public List<ClsProducto> Buscar(string producto)
        {
            return Producto_SP.BuscarProducto(producto);
        }
    }
}
