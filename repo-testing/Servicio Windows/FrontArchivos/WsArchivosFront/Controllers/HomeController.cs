using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WsArchivosFront.Models;
using ClienteAPI;
using Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace WsArchivosFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("clientes")==null)
                {
                    HttpContext.Session.SetString("clientes", JsonConvert.SerializeObject(new sesion()));
                }
                sesion clsSession = JsonConvert.DeserializeObject<sesion>(HttpContext.Session.GetString("clientes"));
                if (clsSession.filtro == false)
                {
                    ViewBag.Lista = new InvocaAPI().ListarClientes();
                }
                else
                {
                    ViewBag.Lista = clsSession.Clientes;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
            return View();
        }
        [HttpPost]
        public int ActualizarCliente()
        {
            Cliente c = new Cliente()
            {
                Cedula = Request.Form["Cedula"],
                Nombre = Request.Form["Name"],
                Apellido = Request.Form["Last"],
                Edad = int.TryParse(Request.Form["Edad"], out int edad) ? edad : 0
            };
            sesion clsSession = JsonConvert.DeserializeObject<sesion>(HttpContext.Session.GetString("clientes"));
            clsSession.filtro = false;
            HttpContext.Session.SetString("clientes", JsonConvert.SerializeObject(clsSession));
            return new InvocaAPI().ActualizarCliente(c);
        }
        [HttpPost]
        public List<Cliente> FiltrarClientes()
        {
            Cliente c = new Cliente()
            {
                Cedula = Request.Form["Cedula"],
                Nombre = Request.Form["Name"],
                Apellido = Request.Form["Last"],
                Edad = int.TryParse(Request.Form["Edad"], out int edad) ? edad : -1
            };
            sesion clsSession = new sesion();
            clsSession.Clientes= new InvocaAPI().FiltrarClientes(c);
            clsSession.filtro = true;
            HttpContext.Session.SetString("clientes", JsonConvert.SerializeObject(clsSession));
            return clsSession.Clientes;
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
