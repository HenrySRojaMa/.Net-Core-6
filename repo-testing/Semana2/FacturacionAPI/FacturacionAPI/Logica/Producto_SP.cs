using FacturacionAPI.DataBase;
using FacturacionAPI.Models;
using FacturacionAPI.Validaciones;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FacturacionAPI
{
    public class Producto_SP
    {        
        public static ClsTransaccionAPI IngresarProducto(ClsProducto ObjProducto)
        {
            ClsTransaccionAPI resp = new ClsTransaccionAPI();
            bool flag = ClsValidaciones.validarProducto(ObjProducto);
            if (!flag)
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("Producto_SP", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Metodo", 1);
                    cmd.Parameters.AddWithValue("@Codigo", ObjProducto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", ObjProducto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", ObjProducto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", ObjProducto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", ObjProducto.Stock);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conexion.Open();
                    da.Fill(dt);
                    conexion.Close();

                    if (dt.Rows.Count == 1)
                    {
                        resp.Codigo = dt.Rows[0]["Codigo"].ToString();
                        resp.Mensaje = dt.Rows[0]["Mensaje"].ToString();
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            resp.Codigo = dr["Codigo"].ToString();
                            resp.Mensaje = dr["Mensaje"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    resp.Codigo = "999"; resp.Mensaje = ex.Message.ToString();
                }
            }
            else
            {
                resp.Codigo = "666";
                resp.Mensaje = "Faltan campos necesarios";
            }
            return resp;
        }
        //----------------
        public static ClsTransaccionAPI ActualizarProducto(ClsProducto ObjProducto)
        {
            ClsTransaccionAPI resp = new ClsTransaccionAPI();
            bool flag = ClsValidaciones.validarProducto(ObjProducto);
            if (!flag)
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("Producto_SP", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Metodo", 3);
                    cmd.Parameters.AddWithValue("@Codigo", ObjProducto.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", ObjProducto.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", ObjProducto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", Convert.ToDecimal(ObjProducto.Precio));
                    cmd.Parameters.AddWithValue("@Stock", ObjProducto.Stock);

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
            }
            else
            {
                resp.Codigo = "666";
                resp.Mensaje = "Faltan campos necesarios";
            }
            return resp;
        }
        //----------------
        public static List<ClsProducto> ListarProducto()
        {            
            try
            {                
                List<ClsProducto> list = new List<ClsProducto>();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion();                ;
                SqlCommand cmd = new SqlCommand("Producto_SP", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 2);
                cmd.Parameters.AddWithValue("@Codigo", null);
                cmd.Parameters.AddWithValue("@Nombre", null);
                cmd.Parameters.AddWithValue("@Descripcion", null);
                cmd.Parameters.AddWithValue("@Precio", null);
                cmd.Parameters.AddWithValue("@Stock", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ClsProducto producto = new ClsProducto(); //limpiar
                    producto.Codigo = int.Parse(dr["Codigo"].ToString());
                    producto.Nombre = dr["Nombre"].ToString();
                    producto.Descripcion = dr["Descripcion"].ToString();
                    producto.Precio = double.Parse(dr["Precio"].ToString());                    
                    producto.Stock = int.Parse(dr["Stock"].ToString());
                    list.Add(producto);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<ClsProducto> BuscarProducto(string produto)
        {
            try
            {
                List<ClsProducto> list = new List<ClsProducto>();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion(); ;
                SqlCommand cmd = new SqlCommand("Producto_SP", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 4);
                cmd.Parameters.AddWithValue("@Codigo", null);
                cmd.Parameters.AddWithValue("@Nombre", produto);
                cmd.Parameters.AddWithValue("@Descripcion", null);
                cmd.Parameters.AddWithValue("@Precio", null);
                cmd.Parameters.AddWithValue("@Stock", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ClsProducto producto = new ClsProducto(); //limpiar
                    producto.Codigo = int.Parse(dr["Codigo"].ToString());
                    producto.Nombre = dr["Nombre"].ToString();
                    producto.Descripcion = dr["Descripcion"].ToString();
                    producto.Precio = double.Parse(dr["Precio"].ToString());
                    producto.Stock = int.Parse(dr["Stock"].ToString());
                    list.Add(producto);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}