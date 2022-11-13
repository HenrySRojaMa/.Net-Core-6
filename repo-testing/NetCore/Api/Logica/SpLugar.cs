using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Logica
{
    public class SpLugar
    {
        public List<Lugar> ListarProvincias()
        {
            try
            {
                List<Lugar> list = new List<Lugar>();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion(); ;
                SqlCommand cmd = new SqlCommand("Extras", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 2);
                cmd.Parameters.AddWithValue("@ciudad", null);
                cmd.Parameters.AddWithValue("@codigoCliente", null);
                cmd.Parameters.AddWithValue("@provincia", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Lugar l = new Lugar(); //limpiar
                    l.CodigoP = int.Parse(dr["CodigoP"].ToString());
                    l.NombreP = dr["Nombre"].ToString();
                    l.CodigoC = 0;
                    l.NombreC = "";
                    list.Add(l);
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public List<Lugar> ListarCiudades(int provincia)
        {
            try
            {
                List<Lugar> list = new List<Lugar>();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion(); ;
                SqlCommand cmd = new SqlCommand("Extras", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 3);
                cmd.Parameters.AddWithValue("@ciudad", null);
                cmd.Parameters.AddWithValue("@codigoCliente", null);
                cmd.Parameters.AddWithValue("@provincia", provincia);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Lugar l = new Lugar(); //limpiar
                    l.CodigoP = int.Parse(dr["CodigoP"].ToString());
                    l.NombreP = "";
                    l.CodigoC = int.Parse(dr["CodigoC"].ToString());
                    l.NombreC = dr["Nombre"].ToString();
                    list.Add(l);
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
