using Microsoft.Extensions.Configuration;
using Persona_CRUD.Interfaces;
using Persona_CRUD.Models;
//using PersonaSoapWebService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Persona_CRUD.Services
{
    public class PersonaService : IPersona
    {

        private Conexion conexion;

        public PersonaService(IConfiguration configuration)
        {
            this.conexion = new Conexion(configuration.GetConnectionString("Back"));
        }

        public async Task<Response> ActualizarCliente(Persona persona)
        {
            Response rpta = new Response();
            object obj = new object();
            try
            {
                PersonaSoapWebService.PersonaWebServiceSoapClient SoapClient = new PersonaSoapWebService.PersonaWebServiceSoapClient(PersonaSoapWebService.PersonaWebServiceSoapClient.EndpointConfiguration.PersonaWebServiceSoap);
                var temp = SoapClient.ActualizarPersonaAsync(new PersonaSoapWebService.Persona {
                    Cedula=persona.Cedula,
                    Apellido=persona.Apellido,
                    Correo=persona.Correo,
                    Direccion=persona.Direccion,
                    Nombre=persona.Nombre,
                    Telefono=persona.Telefono
                }).Result;
                rpta.Codigo = temp.Body.ActualizarPersonaResult.Codigo;
                rpta.Mensaje = temp.Body.ActualizarPersonaResult.Mensaje;
                obj= temp.Body.ActualizarPersonaResult.obj;
                return rpta;
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = ex.Message;
                return rpta;
            }
        }

        public async Task<Persona> ConsultarPersona(string cedula)
        {
            Persona rpta = new Persona();
            try
            {
                SqlCommand cmd = new SqlCommand("ConsultarCliente", conexion.Link);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", cedula);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Link.Open();
                da.Fill(dt);
                conexion.Link.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    rpta.Cedula = dr["Cedula"].ToString();
                    rpta.Nombre = dr["Nombre"].ToString();
                    rpta.Apellido = dr["Apellido"].ToString();
                    rpta.Telefono = dr["Telefono"].ToString();
                    rpta.Direccion = dr["Direccion"].ToString();
                    rpta.Correo = dr["Correo"].ToString();
                }
                return rpta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> IngresarPersona(Persona persona)
        {
            Response rpta = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("IngresarCliente", conexion.Link);
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
                }
                return rpta;
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = ex.Message;
                return rpta;
            }
        }
    }
}
