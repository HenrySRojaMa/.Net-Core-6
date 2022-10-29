using SoapWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SoapWebService.Services
{
    public class PersonaService
    {
        private Conexion conexion;

        public PersonaService()
        {
            this.conexion = new Conexion();
        }

        public Response ActualizarPersona(Persona persona)
        {
            Response rpta = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("ActualizarCliente", conexion.Link);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", persona.Cedula);
                cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", persona.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", persona.Direccion);
                cmd.Parameters.AddWithValue("@Correo", persona.Correo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Link.Open();
                da.Fill(dt);
                conexion.Link.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    rpta.Codigo = dr["Codigo"].ToString();
                    rpta.Mensaje = dr["Mensaje"].ToString();
                    rpta.obj = persona;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = ex.Message;
                rpta.obj = persona;
                return rpta;
            }
        }

    }
}