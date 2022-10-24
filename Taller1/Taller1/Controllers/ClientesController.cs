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
    public class ClientesController : ControllerBase
    {
        private readonly IClientes _clientes;

        public ClientesController(IClientes cliente)
        {
            _clientes=cliente;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
            return await _clientes.ReadCliente(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            return Ok(await _clientes.UpdateCliente(id, cliente));
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            return Ok(await _clientes.CreateCliente(cliente));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            return Ok( await _clientes.DeleteCliente(id));
        }

    }
}
