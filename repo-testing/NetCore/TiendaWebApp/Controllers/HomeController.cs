using ClienteAPI;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TiendaWebApp.Models;

namespace TiendaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /*
        public IActionResult Index()
        {
            return View("~/Views/Home/Login.cshtml");
        }*/

        public IActionResult Index()
        {
            SessionUsuario clsSession = null;
            if (HttpContext.Session.GetString("DatosUsuario")!=null)
            {
                clsSession = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            }
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
                clsSession.Token = rpta.Mensaje;
                clsSession.cliente = api.BuscarClienteLoggeado(rpta.Mensaje, usurio);
                rpta.Mensaje = "Usuario Autenticado";
                /*var filePath = "C:\\Users\\DELL\\Documents\\repo-testing\\Semana2\\FacturacionWebApp\\FacturacionWebApp\\Content\\img\\usuario.jpeg";
                byte[] fileBytes=null;
                if (System.IO.File.Exists(filePath))
                {
                    fileBytes = System.IO.File.ReadAllBytes(filePath);
                }
                clsSession.Foto = Convert.ToBase64String(fileBytes);*/
                HttpContext.Session.SetString("DatosUsuario", JsonConvert.SerializeObject(clsSession));
            }
            return Json(rpta);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View("~/Views/Home/Login.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
