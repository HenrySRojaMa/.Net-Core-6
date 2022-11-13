using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_ASMX.Logica;
using WS_ASMX.Models;

namespace WS_ASMX
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        //Controller
        [WebMethod (Description ="NEO-1234 Este metodo es una consulta ")]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod(Description = "NEO-1234 Este metodo es un metodo de prueba ")]
        public string metodoPrueba() {
            return "metodo de prueba"; 
        }

        [WebMethod(Description = "WS_ASMX-CLIENTE: Este metodo ingresa clientes :)")]
        public void Insertar(string identificacion, string nombre, string apellido, int edad, string estadocivil, out string Codigo, out string Mensaje)
        {

            Codigo = string.Empty;
            Mensaje = string.Empty;

            ClsCliente objCliente = new ClsCliente();
            ClsTransaccion rpta = new ClsTransaccion();
            objCliente.Identificacion = identificacion;

            SpCliente interfaz = new SpCliente();
            rpta = interfaz.Insertar(objCliente);

            Codigo = rpta.Codigo;
            Mensaje = rpta.Mensaje;            
        }

        [WebMethod(Description = "WS_ASMX-CLIENTE_Lista: Este metodo Lista los clientes :)")]
        public void Listar(out List<object[]> lista)
        {
            SpCliente cliente = new SpCliente();
            lista = new List<object[]>();
            object[] fila;
            var temp = cliente.Listar();
            foreach (var item in temp)
            {
                fila = new object[6];
                fila[0] = item.ID;
                fila[1] = item.Nombre;
                fila[2] = item.Apellido;
                fila[3] = item.Identificacion;
                fila[4] = item.Edad;
                fila[5] = item.EstadoCivil;
                lista.Add(fila);
            }
        }

        [WebMethod(Description = "WS_ASMX-CLIENTE_CI: Este metodo Busca por identificacion los datos de un cliente :)")]
        public void Buscar(string Identificacion, out string Codigo, out string Mensaje, out ClsCliente cli)
        {            
            Codigo = string.Empty;
            Mensaje = string.Empty;
            SpCliente cliente = new SpCliente();
            ClsTransaccion rpta = new ClsTransaccion();
            cli = new ClsCliente();
            cli = cliente.Buscar(Identificacion);

            if (cli.Nombre == null)
            {

                Codigo = "999";
                Mensaje = "No se encontraron registros con la identificacion";
            }
            else
            {
                Codigo = "000";
                Mensaje = "OK";
            }


        }

        [WebMethod(Description = "WS_ASMX-CLIENTE_Up: Este metodo actualiza los datos de un cliente :)")]
        public void Actualizar(string identificacion, string nombre, string apellido, int edad, string estadocivil, out string Codigo, out string Mensaje)
        {
            Codigo = string.Empty;
            Mensaje = string.Empty;

            ClsCliente objCliente = new ClsCliente()
            {
                ID = 0,
                Nombre = nombre,
                Apellido = apellido,
                Identificacion = identificacion,
                Edad = edad,
                EstadoCivil = Convert.ToChar(estadocivil)
            };

            ClsTransaccion rpta = new ClsTransaccion();

            SpCliente interfaz = new SpCliente();
            rpta = interfaz.Actualizar(objCliente);

            Codigo = rpta.Codigo;
            Mensaje = rpta.Mensaje;

        }


    }
}
