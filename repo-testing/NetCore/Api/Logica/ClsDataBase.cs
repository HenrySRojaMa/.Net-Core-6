using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Logica
{
    class ClsDataBase
    {
        public SqlConnection Connection { get; set; }

        public SqlConnection Conexion()
        {
            //string conexion = Environment.GetEnvironmentVariable("ConexionBD").ToString();
            string conexion = "Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;Initial Catalog=Facturacion;Integrated Security=True";
            return new SqlConnection(conexion);
        }
    }
}
