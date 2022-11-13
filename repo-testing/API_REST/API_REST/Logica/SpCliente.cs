using API_REST.DataBase;
using API_REST.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_REST.Logica
{
    public class SpCliente
    {
        public static ClsTransaccion Insertar(ClsCliente cliente)
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
                    #region prueba
                    /*int respuesta = cmd.ExecuteNonQuery();
                     * if (respuesta==1)
                    {
                        trans.Codigo = "000"; trans.Mensaje = "OK PANA";                    
                    }
                    else
                    {
                        trans.Codigo = "999"; trans.Mensaje = "ERROR PANA";
                    }*/
                    #endregion
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

        public static List<ClsCliente> Listar()
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

        public static ClsCliente Buscar(string Identificacion)
        {            
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
                    temp = new ClsCliente();
                    temp.ID = Convert.ToInt32(dr["ID"].ToString());
                    temp.Nombre = dr["Nombre"].ToString();
                    temp.Apellido = dr["Apellido"].ToString();
                    temp.Identificacion = dr["Identificacion"].ToString();
                    temp.Edad = Convert.ToInt32(dr["Edad"].ToString());
                    temp.EstadoCivil = Convert.ToChar(dr["EstadoCivil"].ToString());
                    
                }
                return temp;

                /* SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {
                     cliente.ID = dr.GetInt32(0);
                     cliente.Nombre = dr.GetString(1);
                     cliente.Apellido = dr.GetString(2);
                     cliente.Identificacion = dr.GetString(3);
                     cliente.Edad = dr.GetInt32(4);
                     cliente.EstadoCivil = char.Parse(dr.GetString(4));
                     cn.Close();
                     return cliente;
                 }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static ClsTransaccion Actualizar( ClsCliente cliente)
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