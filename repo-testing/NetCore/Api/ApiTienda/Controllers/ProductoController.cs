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
    public class ProductoController : ControllerBase
    {
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
