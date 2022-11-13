using Data;
using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LugarController : ControllerBase
    {
        [HttpGet]
        [Route("provincias")]
        public List<Lugar> ListarProvincias()
        {
            return new SpLugar().ListarProvincias();
        }
        [HttpGet]
        [Route("ciudades")]
        public List<Lugar> BuscarCiudades(int CodigoP)
        {
            return new SpLugar().ListarCiudades(CodigoP);
        }
    }
}
