using System.Data.SqlClient;
using System.Web.Configuration;

namespace WS_ASMX.DataBase
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