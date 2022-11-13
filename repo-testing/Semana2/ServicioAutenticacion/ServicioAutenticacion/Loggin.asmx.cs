using ServicioAutenticacion.Logica;
using ServicioAutenticacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ServicioAutenticacion
{
    /// <summary>
    /// Descripción breve de Loggin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Loggin : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod(Description = "WS_ASMX-LOGGIN: Este metodo autentica usuarios")]
        public ClsTransaccion Autenticar(string identificacion, string password)
        {
            //Codigo = string.Empty;
            //Mensaje = string.Empty;
            ClsTransaccion rpta = new ClsTransaccion();
            SpAutenticacion sp = new SpAutenticacion();
            rpta = sp.loggin(new Usuario() { Identificacion = identificacion, Password = password });
            ///// Codigo = rpta.Codigo;
            // Mensaje = rpta.Mensaje;

            return rpta;
        }
    }
}
