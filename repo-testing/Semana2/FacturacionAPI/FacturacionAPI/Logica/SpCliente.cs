using FacturacionAPI.DataBase;
using FacturacionAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacturacionAPI.Logica
{
    public class SpCliente
    {
        public Cliente Buscar(String Id)
        {
            Cliente c = new Cliente();
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("Cliente_SP", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conexion.Open();
                    da.Fill(dt);
                    conexion.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        c.Identificacion = dr["Identificacion"].ToString();
                        c.Nombre = dr["Nombre"].ToString();
                        c.Apellido = dr["Apellido"].ToString();
                        c.Direccion = dr["Direccion"].ToString();
                        c.Telefono = dr["Telefono"].ToString();
                        c.Correo = dr["Correo"].ToString();
                        c.Foto = dr["Foto"].ToString();
                    }
                }
                catch (Exception)
                {
                    c=null;
                }
            }
            else
            {
                c = null;
            }
            return c;
        }
    }
}