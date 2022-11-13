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
    public class ServiciosController : Controller
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
                    return View("~/Views/Servicios/VentanaServicios.cshtml");
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
    }
}
