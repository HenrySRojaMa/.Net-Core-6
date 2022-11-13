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
            string conexion = "Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;User ID=usuarioPrueba;Password=123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            return new SqlConnection(conexion);
        }
    }
}
