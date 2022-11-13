using FacturacionWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionWebApp.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            SessionUsuario clsSession = Session["DatosUsuario"] as SessionUsuario;
            if (clsSession!=null)
            {
                if (clsSession.cliente!=null)
                {
                    ViewBag.UsuarioSesion = clsSession.cliente;
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
    }
}