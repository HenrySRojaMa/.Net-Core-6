using SoapWebService.Models;
using SoapWebService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SoapWebService
{
    /// <summary>
    /// Descripción breve de PersonaWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PersonaWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod(Description ="SoapWebService_Persona_Actualizar: Actualiza los datos de una persona")]
        public Response ActualizarPersona(Persona persona)
        {
            return new PersonaService().ActualizarPersona(persona);
        }
    }
}
