using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Logica
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
                        c.Ciudad= dr["Ciudad"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    c = null;
                }
            }
            else
            {
                c = null;
            }
            return c;
        }

        public ClsTransaccionAPI ActualizarCliente(Cliente c)
        {
            ClsTransaccionAPI resp = new ClsTransaccionAPI();
            /*bool flag = ClsValidaciones.validarProducto(ObjProducto);
            if (!flag)
            {
            }
            else
            {
                resp.Codigo = "666";
                resp.Mensaje = "Faltan campos necesarios";
            }*/
            try
            {
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion();
                SqlCommand cmd = new SqlCommand("Extras", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 1);
                cmd.Parameters.AddWithValue("@ciudad", int.Parse(c.Ciudad));
                cmd.Parameters.AddWithValue("@codigoCliente", c.Identificacion);
                cmd.Parameters.AddWithValue("@provincia", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    resp.Codigo = dr["Codigo"].ToString();
                    resp.Mensaje = dr["Mensaje"].ToString();
                }
            }
            catch (Exception ex)
            {
                resp.Codigo = "999"; resp.Mensaje = ex.Message.ToString();
            }
            return resp;
        }
    }
}
