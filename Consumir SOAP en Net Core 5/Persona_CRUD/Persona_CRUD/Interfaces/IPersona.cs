using Persona_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persona_CRUD.Interfaces
{
    public interface IPersona
    {
        Task<Response> IngresarPersona(Persona persona);
        Task<Persona> ConsultarPersona(string cedula);
        Task<Response> ActualizarCliente(Persona persona);
    }
}
