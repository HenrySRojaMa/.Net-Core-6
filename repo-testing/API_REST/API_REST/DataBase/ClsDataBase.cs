using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace API_REST.DataBase
{
    public class ClsDataBase
    {
      
        public SqlConnection Connection { get; set; }

        public SqlConnection Conexion()
        {

          return new SqlConnection(WebConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString);

        }
        /*
        private void Validar(ref ClsDataBase DB)
        {
            if (DB.Connection.State == ConnectionState.Closed)  
            {
                DB.Connection.Open();               
            }
            else
            {
                DB.Connection.Close();
                DB.Connection.Dispose();
            }
        }
        public void PrepararBaseDatos(ref ClsDataBase DB)
        {
            Conexion(); //crear conexion  
            Validar(ref DB); //validar conexion     
        }*/

    }
}