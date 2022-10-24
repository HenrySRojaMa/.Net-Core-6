using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taller1.Interfaces;
using Taller1.Models;

namespace Taller1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturas _facturas;

        public FacturasController(IFacturas facturas)
        {
            _facturas = facturas;
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cabecera>> GetCabecera(int id)
        {
            return null;
        }

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cabecera>> PostCabecera(Cabecera cabecera)
        {
            return Ok(await _facturas.CreateFactura(cabecera));
        }
    }
}
