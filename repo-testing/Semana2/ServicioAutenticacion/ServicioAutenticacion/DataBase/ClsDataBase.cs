using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ServicioAutenticacion.DataBase
{
    public class ClsDataBase
    {
        public SqlConnection Connection { get; set; }

        public SqlConnection Conexion()
        {
            //return new SqlConnection(WebConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
            return new SqlConnection("Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;Initial Catalog=Facturacion;Integrated Security=True");
        }
    }
}