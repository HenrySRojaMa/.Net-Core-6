using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerProductos()
        {
            HttpClient client = new();
            var response = client.GetAsync("https://localhost:44303/api/Productos").Result;
            if (response.IsSuccessStatusCode)
            {
                var rpta = response.Content.ReadAsStringAsync().Result;
                return Json(JsonConvert.DeserializeObject<List<Producto>>(rpta));
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult GuardarProducto(Producto producto)
        {
            HttpClient client = new();
            var response = client.PostAsync("https://localhost:44303/api/Productos/GuardarProducto",
                new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                var rpta = response.Content.ReadAsStringAsync().Result;
                return Json(rpta);
            }
            return Json(null);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
