using ApiArchivos.Interfaces;
using ApiArchivos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ApiArchivos.Services
{
    public class ArchivoService : IArchivo
    {

        private readonly IConfiguration _configuration;

        public ArchivoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response> SubirArchivo(IFormFile file)
        {
            Response rpta = new Response();
            Archivo archivo = new Archivo
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Length = (double)file.Length/1024 + "",
                FilePath = _configuration["FilePath"] + file.FileName
            };
            using (var stream = System.IO.File.Create(archivo.FilePath))
            {
                await file.CopyToAsync(stream);
            }
            SqlConnection Link = new SqlConnection(_configuration.GetConnectionString("Back"));
            try
            {
                SqlCommand cmd = new SqlCommand("SubirArchivo", Link);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@length", archivo.Length);
                cmd.Parameters.AddWithValue("@filePath", archivo.FilePath);
                cmd.Parameters.AddWithValue("@fileName", archivo.FileName);
                cmd.Parameters.AddWithValue("@contentType", archivo.ContentType);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                Link.Open();
                da.Fill(dt);
                Link.Close();

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

        public async Task<Response> DescargarArchivo(int Id)
        {
            SqlConnection Link = new SqlConnection(_configuration.GetConnectionString("Back"));
            Archivo archivo = new Archivo();
            Response rpta =new Response();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("DescargarArchivo", Link);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                

                Link.Open();
                da.Fill(dt);
                Link.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    archivo.Id = int.Parse(dr["Id"].ToString());
                    archivo.Length = dr["Length"].ToString();
                    archivo.FilePath = dr["FilePath"].ToString();
                    archivo.FileName = dr["FileName"].ToString();
                    archivo.ContentType = dr["ContentType"].ToString();
                }

                if (System.IO.File.Exists(archivo.FilePath))
                {
                    archivo.FileBytes = System.IO.File.ReadAllBytes(archivo.FilePath);
                    rpta.Objeto = archivo;
                    return rpta;
                }
                else
                {
                    rpta.Codigo = "999";
                    rpta.Mensaje = "Archivo extraviado";
                    return rpta;
                }
            }
            catch (Exception)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    rpta.Codigo = dr["Codigo"].ToString();
                    rpta.Mensaje = dr["Mensaje"].ToString();
                }
                return rpta;
            }
        }

    }
}
