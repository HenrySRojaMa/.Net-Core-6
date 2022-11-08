using ApiArchivos.Interfaces;
using ApiArchivos.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiArchivos.Services
{
    public class UserService : IUser
    {
        //private readonly IPasswordHasher<User> _passwordHasher;
        private SqlConnection Link;
        private readonly IDataProtector _dataProtector;

        /*public UserService(IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            Link = new SqlConnection(configuration.GetConnectionString("Back"));
        }*/
        public UserService(IDataProtectionProvider protectionProvider, IConfiguration configuration)
        {
            this._dataProtector = protectionProvider.CreateProtector(configuration["Key"]);
            Link = new SqlConnection(configuration.GetConnectionString("Back"));
        }
        public async Task<Response> RegistrarUsuario(User user)
        {
            Response rpta = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("RegistrarUsuario", Link);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cedula", user.Cedula);
                cmd.Parameters.AddWithValue("@psw", this._dataProtector.Protect(user.Password));
                //cmd.Parameters.AddWithValue("@psw", this._passwordHasher.HashPassword(user,user.Password));
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

        public async Task<Response> ConsultarUsuario(User user)
        {
            Response rpta = new Response();
            try
            {
                SqlCommand cmd = new SqlCommand("ConsultarUsuario", Link);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cedula", user.Cedula);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                Link.Open();
                da.Fill(dt);
                Link.Close();

                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        rpta.Objeto = new User
                        {
                            Cedula = dr["Cedula"].ToString(),
                            //Password = dr["Password"].ToString()
                            Password = this._dataProtector.Unprotect(dr["Password"].ToString())
                        };
                        //rpta.Mensaje = dr["Password"].ToString();
                    }
                    /*var resultado = _passwordHasher.VerifyHashedPassword(user,rpta.Mensaje,user.Password);
                    if (resultado == PasswordVerificationResult.Success)
                    {
                        rpta.Codigo = "000";
                        rpta.Mensaje = "Credenciales correctas";
                    }*/
                }
                catch (Exception ex)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        rpta.Codigo = dr["Codigo"].ToString();
                        rpta.Mensaje = dr["Mensaje"].ToString();
                    }
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
