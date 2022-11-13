using FacturacionWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionWebApp.Controllers
{
    public class ServiciosController : Controller
    {
        // GET: Servicios
        public ActionResult Index()
        {
            SessionUsuario clsSession = Session["DatosUsuario"] as SessionUsuario;
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