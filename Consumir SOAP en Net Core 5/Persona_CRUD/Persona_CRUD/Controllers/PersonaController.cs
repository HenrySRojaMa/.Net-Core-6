using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Persona_CRUD.Interfaces;
using Persona_CRUD.Models;
using Persona_CRUD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persona_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        
        private readonly IPersona _persona;

        public PersonaController(IPersona persona)
        {
            this._persona = persona;
        }
        
        // GET api/<PersonaController>/5
        [HttpGet("{id}")]
        public async Task<Persona> Get(string id)
        {
            return await _persona.ConsultarPersona(id);
        }

        // POST api/<PersonaController>
        [HttpPost]
        public async Task<Response> IngresarPersona([FromBody] Persona persona)
        {
            return await _persona.IngresarPersona(persona);
        }

        [HttpPut]
        public async Task<Response> ActualizarPersona([FromBody] Persona persona)
        {
            return await _persona.ActualizarCliente(persona);
        }

    }
}
