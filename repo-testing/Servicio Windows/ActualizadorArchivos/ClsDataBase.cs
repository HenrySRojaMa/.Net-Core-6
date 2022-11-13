using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorArchivos
{
    class ClsDataBase
    {/*
        public SqlConnection link { get; set; }

        public Conexion()
        {
            this.link = new SqlConnection("Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;User ID=usuarioPrueba;Password=123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //this.link = new SqlConnection("");
        }
        public void Conectar()
        {
            try
            {
                link.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Desconectar()
        {
            try
            {
                link.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }*/
        public SqlConnection Connection { get; set; }

        public SqlConnection Conexion()
        {
            string conexion = "Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;User ID=usuarioPrueba;Password=123456;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            return new SqlConnection(conexion);
        }
    }
}
