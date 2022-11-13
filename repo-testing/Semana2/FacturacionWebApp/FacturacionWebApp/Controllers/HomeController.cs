using FacturacionWebApp.ClienteAPI;
using FacturacionWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FacturacionWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SessionUsuario clsSession = Session["DatosUsuario"] as SessionUsuario;
            if (clsSession != null)
            {
                if (clsSession.cliente != null)
                {
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

        [HttpPost]
        public JsonResult ProcesoLogin()
        {
            string usurio = Request.Form["usuario"];
            string pwd = Request.Form["pwd"];

            Usuario rqUsuario = new Usuario();
            rqUsuario.Identificacion = usurio;
            rqUsuario.Password = pwd;

            ClsTransaccion rpta = new ClsTransaccion();
            InvocaAPI api = new InvocaAPI();
            rpta = api.consultaApi(rqUsuario);
            if (rpta.Codigo == "000")
            {
                SessionUsuario clsSession = new SessionUsuario();
                clsSession.cliente = api.BuscarClienteLoggeado(usurio);
                /*var filePath = "C:\\Users\\DELL\\Documents\\repo-testing\\Semana2\\FacturacionWebApp\\FacturacionWebApp\\Content\\img\\usuario.jpeg";
                byte[] fileBytes=null;
                if (System.IO.File.Exists(filePath))
                {
                    fileBytes = System.IO.File.ReadAllBytes(filePath);
                }
                clsSession.Foto = Convert.ToBase64String(fileBytes);*/
                Session["DatosUsuario"] = clsSession;
            }
            return Json(rpta);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return View("~/Views/Home/Login.cshtml");
        }
    }
}