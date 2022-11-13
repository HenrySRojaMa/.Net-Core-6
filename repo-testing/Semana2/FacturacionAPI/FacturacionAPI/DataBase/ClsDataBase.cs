using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace FacturacionAPI.DataBase
{
    public class ClsDataBase
    {
        public SqlConnection Connection { get; set; }

        public SqlConnection Conexion()
        {
            return new SqlConnection(WebConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }
    }
}