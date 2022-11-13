using ServicioAutenticacion.DataBase;
using ServicioAutenticacion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicioAutenticacion.Logica
{
    public class SpAutenticacion
    {
        public ClsTransaccion loggin (Usuario usuario) {
            ClsTransaccion temp = new ClsTransaccion();
            try
            {
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion();
                SqlCommand cmd = new SqlCommand("loggin", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cedula", usuario.Identificacion);
                cmd.Parameters.AddWithValue("@password", usuario.Password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                conexion.Open();
                da.Fill(dt);
                conexion.Close();
                if (dt.Rows.Count == 1)
                {
                    temp.Codigo = dt.Rows[0]["Codigo"].ToString();
                    temp.Mensaje = dt.Rows[0]["Mensaje"].ToString();
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        temp.Codigo = dr["Codigo"].ToString();
                        temp.Mensaje = dr["Mensaje"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                temp.Codigo = "666"; temp.Mensaje = ex.Message.ToString();
            }
            return temp;
        }
    }
}