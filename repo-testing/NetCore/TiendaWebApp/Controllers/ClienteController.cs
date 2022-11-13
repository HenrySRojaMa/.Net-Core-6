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
    public class ClienteController : Controller
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
                    ViewBag.UsuarioSesion = clsSession.cliente;
                    ViewBag.Provincias = this.ListarProvincias();
                    return View("~/Views/Cliente/Cliente.cshtml");
                }
                else
                {
                    return RedirectToAction("Logout", "Home");
                }
            }
            else
            {
                return RedirectToAction("Logout", "Home");
            }
        }

        public IActionResult Actualizar(string ciudad)
        {
            InvocaAPI api = new InvocaAPI();
            SessionUsuario clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            //int codigo, stock; double precio;
            Cliente c = new Cliente()
            {
                Identificacion = clsSession.cliente.Identificacion,
                Ciudad = ciudad
            };
            var rpta = api.ActualizarCliente(clsSession.Token,c);
            if (rpta.Codigo=="000")
            {
                return RedirectToAction("Logout", "Home");
            }
            else
            {
                return Json(rpta);
            }
        }

        public List<Lugar> ListarProvincias()
        {
            var session = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            return new InvocaAPI().ListarProvincias(session.Token);
        }

        public JsonResult BuscarCiudades(int codigop)
        {
            var session = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            return Json(new InvocaAPI().BuscarCiudades(session.Token,codigop));
        }
    }
}
