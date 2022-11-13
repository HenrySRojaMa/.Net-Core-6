using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Logica
{
    public class SpFactura
    {
        public TransaccionID IngresarCabecera(Factura f)
        {
            TransaccionID temp = new TransaccionID();
            if (!Utilitarios.validaFactura(f))
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("Factura", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Metodo", 1);
                    cmd.Parameters.AddWithValue("@codigo", f.Codigo);
                    cmd.Parameters.AddWithValue("@codigoCliente", f.CodigoCliente);
                    cmd.Parameters.AddWithValue("@total", f.Total);
                    cmd.Parameters.AddWithValue("@descuento", f.Descuento);
                    cmd.Parameters.AddWithValue("@iva", f.IVA);
                    cmd.Parameters.AddWithValue("@totalApagar", f.TotalaPagar);
                    cmd.Parameters.AddWithValue("@codigoProducto", null);
                    cmd.Parameters.AddWithValue("@cantidad", null);
                    cmd.Parameters.AddWithValue("@subtotal", null);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conexion.Open();
                    da.Fill(dt);
                    conexion.Close();
                    if (dt.Rows.Count == 1)
                    {
                        temp.Codigo = dt.Rows[0]["Codigo"].ToString();
                        temp.Mensaje = dt.Rows[0]["Mensaje"].ToString();
                        temp.IdFactura = int.Parse(dt.Rows[0]["IdFactura"].ToString());
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            temp.Codigo = dr["Codigo"].ToString();
                            temp.Mensaje = dr["Mensaje"].ToString();
                            temp.IdFactura = int.Parse(dr["IdFactura"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    temp.Codigo = "666"; temp.Mensaje = ex.Message.ToString(); temp.IdFactura = 0;
                }
            }
            else
            {
                temp.Codigo = "666";
                temp.Mensaje = "Faltan campos necesarios";
                temp.IdFactura = 0;
            }
            return temp;
        }

        public ClsTransaccionAPI IngresarDetalles(List<Detalle> Detalles)
        {
            ClsTransaccionAPI temp = new ClsTransaccionAPI();
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion();
                SqlCommand cmd = new SqlCommand("Factura", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var item in Detalles)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Metodo", 2);
                    cmd.Parameters.AddWithValue("@codigo", item.CodigoFactura);
                    cmd.Parameters.AddWithValue("@codigoCliente", null);
                    cmd.Parameters.AddWithValue("@total", null);
                    cmd.Parameters.AddWithValue("@descuento", null);
                    cmd.Parameters.AddWithValue("@iva", null);
                    cmd.Parameters.AddWithValue("@totalApagar", null);
                    cmd.Parameters.AddWithValue("@codigoProducto", item.CodigoProducto);
                    cmd.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmd.Parameters.AddWithValue("@subtotal", item.Subtotal);

                    da = new SqlDataAdapter(cmd);
                    dt = new DataTable();

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
            }
            catch (Exception ex)
            {
                temp.Codigo = "666"; temp.Mensaje = ex.Message;
            }
            return temp;
        }

        public Factura2 Consultar(int codigoFactura)
        {
            Factura2 f = new Factura2();
            List<Detalle2> listaDetalle = new List<Detalle2>();
            if (codigoFactura > 0)
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("Factura", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Metodo", 3);
                    cmd.Parameters.AddWithValue("@codigo", codigoFactura);
                    cmd.Parameters.AddWithValue("@codigoCliente", null);
                    cmd.Parameters.AddWithValue("@total", null);
                    cmd.Parameters.AddWithValue("@descuento", null);
                    cmd.Parameters.AddWithValue("@iva", null);
                    cmd.Parameters.AddWithValue("@totalApagar", null);
                    cmd.Parameters.AddWithValue("@codigoProducto", null);
                    cmd.Parameters.AddWithValue("@cantidad", null);
                    cmd.Parameters.AddWithValue("@subtotal", null);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conexion.Open();
                    da.Fill(dt);
                    conexion.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        f.Codigo = codigoFactura;
                        f.Fecha = dr["Fecha"].ToString();
                        f.CodigoCliente = int.Parse(dr["CodigoCliente"].ToString());
                        f.Nombre = dr["Nombre"].ToString();
                        f.Apellido = dr["Apellido"].ToString();
                        f.Total = double.Parse(dr["Total"].ToString());
                        f.Descuento = double.Parse(dr["Descuento"].ToString());
                        f.IVA = double.Parse(dr["IVA"].ToString());
                        f.TotalaPagar = double.Parse(dr["TotalAPagar"].ToString());
                        Detalle2 tempDetalle = new Detalle2();
                        tempDetalle.CodigoProducto = int.Parse(dr["codigoProducto"].ToString());
                        tempDetalle.CodigoFactura = codigoFactura;
                        tempDetalle.Nombre = dr["NombreProducto"].ToString();
                        tempDetalle.Cantidad = int.Parse(dr["Cantidad"].ToString());
                        tempDetalle.Subtotal = double.Parse(dr["Subtotal"].ToString());
                        listaDetalle.Add(tempDetalle);
                    }
                    f.Detalle = listaDetalle;
                }
                catch (Exception ex)
                {
                    f = null;
                }
            }
            else
            {
                f = null;
            }
            return f;
        }
    }
}
