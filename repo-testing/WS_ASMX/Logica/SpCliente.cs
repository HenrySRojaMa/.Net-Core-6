using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WS_ASMX.DataBase;
using WS_ASMX.Models;

namespace WS_ASMX.Logica
{
    public class SpCliente
    {
        public  ClsTransaccion Insertar(ClsCliente cliente)
        {
            ClsTransaccion temp = new ClsTransaccion();
            bool flag = Utilitarios.validaCliente(cliente);
            if (!flag)
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("procedimientosClientes", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Metodo", 1);
                    cmd.Parameters.AddWithValue("@Id", null);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);
                    cmd.Parameters.AddWithValue("@Edad", cliente.Edad);
                    cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);

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
            }
            else
            {
                temp.Codigo = "666";
                temp.Mensaje = "Faltan campos necesarios";
            }
            return temp;
        }

        public List<ClsCliente> Listar()
        {
            try
            {
                List<ClsCliente> lista = new List<ClsCliente>();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion();
                SqlCommand cmd = new SqlCommand("procedimientosClientes", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Metodo", 2);
                cmd.Parameters.AddWithValue("@Id", null);
                cmd.Parameters.AddWithValue("@Nombre", null);
                cmd.Parameters.AddWithValue("@Apellido", null);
                cmd.Parameters.AddWithValue("@Identificacion", null);
                cmd.Parameters.AddWithValue("@Edad", null);
                cmd.Parameters.AddWithValue("@EstadoCivil", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ClsCliente temp = new ClsCliente();
                    temp.ID = Convert.ToInt32(dr["ID"].ToString());
                    temp.Nombre = dr["Nombre"].ToString();
                    temp.Apellido = dr["Apellido"].ToString();
                    temp.Identificacion = dr["Identificacion"].ToString();
                    temp.Edad = Convert.ToInt32(dr["Edad"].ToString());
                    temp.EstadoCivil = Convert.ToChar(dr["EstadoCivil"].ToString());
                    lista.Add(temp);
                }

                return lista;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public ClsCliente Buscar(string Identificacion)
        {
            ClsCliente rpta = new ClsCliente(); 
            try
            {
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion();
                SqlCommand cmd = new SqlCommand("procedimientosClientes", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 3);
                cmd.Parameters.AddWithValue("@Id", null);
                cmd.Parameters.AddWithValue("@Nombre", null);
                cmd.Parameters.AddWithValue("@Apellido", null);
                cmd.Parameters.AddWithValue("@Identificacion", Identificacion);
                cmd.Parameters.AddWithValue("@Edad", null);
                cmd.Parameters.AddWithValue("@EstadoCivil", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                conexion.Open();
                da.Fill(dt);
                conexion.Close();
                ClsCliente temp = null;
                foreach (DataRow dr in dt.Rows)
                {
                    rpta = new ClsCliente();
                    rpta.ID = Convert.ToInt32(dr["ID"].ToString());
                    rpta.Nombre = dr["Nombre"].ToString();
                    rpta.Apellido = dr["Apellido"].ToString();
                    rpta.Identificacion = dr["Identificacion"].ToString();
                    rpta.Edad = Convert.ToInt32(dr["Edad"].ToString());
                    rpta.EstadoCivil = Convert.ToChar(dr["EstadoCivil"].ToString());
                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return rpta; 
        }

        public ClsTransaccion Actualizar(ClsCliente cliente)
        {
            ClsTransaccion temp = new ClsTransaccion();
            if (!Utilitarios.validaCliente(cliente))
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("procedimientosClientes", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Metodo", 4);
                    cmd.Parameters.AddWithValue("@Id", null);
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@Identificacion", cliente.Identificacion);
                    cmd.Parameters.AddWithValue("@Edad", cliente.Edad);
                    cmd.Parameters.AddWithValue("@EstadoCivil", cliente.EstadoCivil);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    //int respuesta = cmd.ExecuteNonQuery();
                    conexion.Open();
                    da.Fill(dt);
                    conexion.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        temp.Codigo = dr["Codigo"].ToString();
                        temp.Mensaje = dr["Mensaje"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    temp.Codigo = "666"; temp.Mensaje = ex.Message.ToString();
                }
            }
            else
            {
                temp.Codigo = "666";
                temp.Mensaje = "Faltan campos necesarios";
            }
            return temp;
        }

    }
}