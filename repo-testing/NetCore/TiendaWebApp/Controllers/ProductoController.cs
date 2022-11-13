using ClienteAPI;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaWebApp.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            SessionUsuario clsSession = null;
            if (HttpContext.Session.GetString("DatosUsuario") != null)
            {
                clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            }
            if (clsSession != null)
            {
                if (clsSession.cliente != null)
                {
                    InvocaAPI api = new InvocaAPI();
                    ViewBag.Lista = api.ListarProductos(clsSession.Token);
                    return View("~/Views/Producto/Producto.cshtml");
                }
                else
                {
                    return View("~/Views/Home/Login.cshtml");
                }
            }
            else
            {
                return View("~/Views/Home/Login.cshtml");
            }
        }
        [HttpPost]
        public JsonResult IngresarProducto()
        {
            var session = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            ClsTransaccion rpta = new ClsTransaccion();
            InvocaAPI api = new InvocaAPI();
            int codigo, stock; double precio;
            Producto p = new Producto()
            {
                Codigo = int.TryParse(Request.Form["Code"], out codigo) ? codigo : 0,
                Descripcion = Request.Form["Descripcion"],
                Nombre = Request.Form["Name"],
                Precio = double.TryParse(Request.Form["Precio"], out precio) ? precio : 0,
                Stock = int.TryParse(Request.Form["Stock"], out stock) ? stock : 0,
            };
            rpta = api.IngresarProducto(session.Token,p);
            return Json(rpta);
        }
        [HttpPost]
        public JsonResult ActualizarProducto()
        {
            var session = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            ClsTransaccion rpta = new ClsTransaccion();
            InvocaAPI api = new InvocaAPI();
            int codigo, stock; double precio;
            Producto p = new Producto()
            {
                Codigo = int.TryParse(Request.Form["Code"], out codigo) ? codigo : 0,
                Descripcion = Request.Form["Descripcion"],
                Nombre = Request.Form["Name"],
                Precio = double.TryParse(Request.Form["Precio"], out precio) ? precio : 0,
                Stock = int.TryParse(Request.Form["Stock"], out stock) ? stock : 0
            };
            rpta = api.ActualizarProducto(session.Token,p);
            return Json(rpta);
        }


    }
}
