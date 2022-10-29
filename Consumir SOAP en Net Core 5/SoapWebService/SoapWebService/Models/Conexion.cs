using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SoapWebService.Models
{
    public class Conexion
    {
        public SqlConnection Link { get; set; }

        public Conexion()
        {
            Link = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);
        }
    }
}