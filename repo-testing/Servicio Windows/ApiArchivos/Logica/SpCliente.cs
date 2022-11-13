using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Data;

namespace Logica
{
    public class SpCliente
    {
        public List<Cliente> ListarClientes()
        {
            List<Cliente> Lista = new List<Cliente>();
            ClsDataBase connex = new ClsDataBase();
            SqlConnection conexion = connex.Conexion();
            SqlCommand cmd = new SqlCommand("SpClientes", conexion);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Metodo", 1);
            cmd.Parameters.AddWithValue("@Cedula", null);
            cmd.Parameters.AddWithValue("@Nombre", null);
            cmd.Parameters.AddWithValue("@Apellido", null);
            cmd.Parameters.AddWithValue("@Edad", null);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            conexion.Open();
            da.Fill(dt);
            conexion.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Lista.Add(new Cliente() {
                    Cedula= dr["Cedula"].ToString(),
                    Nombre = dr["Nombre"].ToString(),
                    Apellido = dr["Apellido"].ToString(),
                    Edad = int.Parse(dr["Edad"].ToString())
                });
            }

            return Lista;
        }

        public List<Cliente> FiltarClientes(Cliente c)
        {
            ClsDataBase connex = new ClsDataBase();
            SqlConnection conexion = connex.Conexion();
            List<Cliente> lista = new List<Cliente>();
            string qwery = "SELECT * FROM Clientes WHERE Cedula LIKE '%" + c.Cedula + "%' and Nombre LIKE '%" + c.Nombre + "%' and Apellido LIKE '%" + c.Apellido + "%' and Edad LIKE '%" + (c.Edad == -1 ? "" : c.Edad+"") + "%'";
            SqlCommand cmd = new SqlCommand(qwery,conexion );
            try
            {
                conexion.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    lista.Add(new Cliente
                    {
                        Cedula = dataReader.GetString(0),
                        Nombre = dataReader.GetString(1),
                        Apellido = dataReader.GetString(2),
                        Edad = dataReader.GetInt32(3)
                    });
                }
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        public int ActualizarClientes(Cliente c)
        {
            int rpta=0;
            if (!Validaciones.validaCliente(c))
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("SpClientes", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Metodo", 3);
                    cmd.Parameters.AddWithValue("@Cedula", c.Cedula);
                    cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                    cmd.Parameters.AddWithValue("@Edad", c.Edad);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    conexion.Open();
                    rpta = cmd.ExecuteNonQuery();
                    conexion.Close();

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    return rpta;
                }
            }
            return rpta;
        }

    }
}
