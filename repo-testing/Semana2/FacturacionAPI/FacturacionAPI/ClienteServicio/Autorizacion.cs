using FacturacionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionAPI.ClienteServicio
{
    public class Autorizacion
    {
        public ClsTransaccionAPI autorizar(Usuario objUsuario)
        {


            ClsTransaccionAPI rpta = new ClsTransaccionAPI();
            ServicioAutenticacion.ClsTransaccion rptaTemp = new ServicioAutenticacion.ClsTransaccion();

            try
            {
                ServicioAutenticacion.Loggin serv = new ServicioAutenticacion.Loggin();
                rptaTemp = serv.Autenticar(objUsuario.Identificacion, objUsuario.Password);

                rpta.Codigo = rptaTemp.Codigo;
                rpta.Mensaje = rptaTemp.Mensaje;
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = "Ocurrio un error " + ex.Message.ToString();  
            }
            return rpta;
        }
    }
}