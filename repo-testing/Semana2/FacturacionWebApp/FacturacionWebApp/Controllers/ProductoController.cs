using FacturacionWebApp.ClienteAPI;
using FacturacionWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionWebApp.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            SessionUsuario clsSession = Session["DatosUsuario"] as SessionUsuario;
            if (clsSession != null)
            {
                if (clsSession.cliente != null)
                {
                    InvocaAPI api = new InvocaAPI();
                    ViewBag.Lista = api.ListarProductos();
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
            ClsTransaccion rpta = new ClsTransaccion();
            InvocaAPI api = new InvocaAPI();
            int codigo, stock; double precio;
            Producto p = new Producto() {
                Codigo = int.TryParse(Request.Form["Code"], out codigo) ? codigo : 0,
                Descripcion = Request.Form["Descripcion"],
                Nombre = Request.Form["Name"],
                Precio = double.TryParse(Request.Form["Precio"], out precio) ? precio : 0,
                Stock = int.TryParse(Request.Form["Stock"], out stock) ? stock : 0,
            };
            rpta = api.IngresarProducto(p);
            return Json(rpta);
        }
        [HttpPost]
        public JsonResult ActualizarProducto()
        {
            ClsTransaccion rpta = new ClsTransaccion();
            InvocaAPI api = new InvocaAPI();
            int codigo, stock; double precio;
            Producto p = new Producto()
            {
                Codigo = int.TryParse(Request.Form["Code"],out codigo)?codigo:0,
                Descripcion = Request.Form["Descripcion"],
                Nombre = Request.Form["Name"],
                Precio = double.TryParse(Request.Form["Precio"],out precio)?precio:0,
                Stock = int.TryParse(Request.Form["Stock"],out stock)?stock:0
            };
            rpta = api.ActualizarProducto(p);
            return Json(rpta);
        }
    }
}