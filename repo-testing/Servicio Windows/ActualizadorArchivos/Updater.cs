using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ActualizadorArchivos
{
    partial class Updater : ServiceBase
    {
        bool ServicioActivo = false, TimeSeted=false;
        int novedades;

        public Updater()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            timer.Start();
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
            timer.Stop();
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var FechaDeHoy = DateTime.Now.ToString().Split(' ')[0].Split('/');
            Configuracion c = this.LeerConfiguracion();
            var Fecha = c.UltimaFecha.Split('/');
            var hora = c.Hora.Split(':');
            if (TimeSeted==false)
            {
                timer.Interval = CalcularSiguienteActualizacion(Fecha, hora, c.Tipo);
                TimeSeted = true;
            }
            else
            {
                if (ServicioActivo) return;
                ServicioActivo = true;
                var Csv = this.LeerCsv(c.Ruta);
                var ClientesActualizar = CargarCliente(Csv);
                int actualizar = ClientesActualizar.Count(), ingresar = Csv.Count - actualizar;
                ActualizarClientes(ClientesActualizar);
                ActualizarLog(ingresar, actualizar);
                ActualizarConfiguracion();
                ServicioActivo = false;
                timer.Interval = CalcularSiguienteActualizacion(FechaDeHoy, hora, c.Tipo);
            }
        }

        public Configuracion LeerConfiguracion()
        {
            /*Conexion conexion = new Conexion();
            Configuracion c = new Configuracion();
            string qwery = "SELECT * FROM ControlEjecucion";
            SqlCommand cmd = new SqlCommand(qwery, conexion.link);
            try
            {
                conexion.Conectar();
                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                c.Tipo = dataReader.GetInt32(0);
                c.Hora = dataReader.GetString(1);
                c.UltimaFecha = dataReader.GetString(2);
                c.Ruta = dataReader.GetString(3);
                conexion.Desconectar();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return c;*/
            try
            {
                Configuracion c = new Configuracion();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion(); ;
                SqlCommand cmd = new SqlCommand("SpWS", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 1);
                cmd.Parameters.AddWithValue("@UltimaFecha", null);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                da.Fill(dt);
                conexion.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    c.Tipo = int.Parse(dr["Tipo"].ToString());
                    c.Hora = dr["Hora"].ToString();
                    c.UltimaFecha = dr["UltimaFecha"].ToString();
                    c.Ruta = dr["Ruta"].ToString();
                }
                return c;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public double CalcularSiguienteActualizacion(string[]fecha, string[]hour, int tipo)
        {
            int dia = int.Parse(fecha[0]), mes = int.Parse(fecha[1]), anio = int.Parse(fecha[2]),
                hora = int.Parse(hour[0]), minuto = int.Parse(hour[1]),
                UltimaActualizacion = mes * 30 + dia,
                SigActualizacion = UltimaActualizacion + tipo,
                DiaHoy = DateTime.Now.Month * 30 + DateTime.Now.Day,
                DiasParaActualizar = SigActualizacion - DiaHoy,
                HoraActualizacion = hora * 60 + minuto,
                HorActual = DateTime.Now.Hour * 60 + DateTime.Now.Minute,
                MinutosParaActualizar = HoraActualizacion - HorActual;
            double 
                DiasRestantesMs = DiasParaActualizar*24*60*60*1000,
                MinutosRestantesMs = MinutosParaActualizar*60*1000 - DateTime.Now.Second * 1000,
                MiliSegundosParaActualizar = DiasRestantesMs + MinutosRestantesMs;
            return MiliSegundosParaActualizar<0?1000:MiliSegundosParaActualizar;
        }

        public void ActualizarConfiguracion()
        {
            try
            {
                Configuracion c = new Configuracion();
                ClsDataBase connex = new ClsDataBase();
                SqlConnection conexion = connex.Conexion(); ;
                SqlCommand cmd = new SqlCommand("SpWS", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 2);
                cmd.Parameters.AddWithValue("@UltimaFecha", DateTime.Now.ToString().Split(' ')[0]);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conexion.Open();
                cmd.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public List<string[]> LeerCsv(string ruta)
        {
            List<string[]> clientes = new List<string[]>();
            var lineas = File.ReadAllLines(ruta);
            foreach (var linea in lineas)
            {
                var cliente = linea.Split(',');
                clientes.Add(cliente);
            }
            return clientes;
        }

        public List<string[]> CargarCliente(List<string[]> archivo)
        {
            novedades =0;
            List<string[]> ActualizarClientes = new List<string[]>();
            string rpta = "";
            foreach (var cliente in archivo)
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("SpClientes", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Metodo", 2);
                    cmd.Parameters.AddWithValue("@Cedula", cliente[0]);
                    cmd.Parameters.AddWithValue("@Nombre", cliente[1]);
                    cmd.Parameters.AddWithValue("@Apellido", cliente[2]);
                    cmd.Parameters.AddWithValue("@Edad", cliente[3]);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conexion.Open();
                    da.Fill(dt);
                    conexion.Close();

                    foreach (DataRow dr in dt.Rows)
                    {
                        rpta = dr["Codigo"].ToString();
                    }
                    if (rpta == "999")
                    {
                        ActualizarClientes.Add(cliente);
                    }
                }
                catch (Exception ex)
                {
                    novedades++;
                    System.Console.WriteLine(ex.Message);
                }
            }
            return ActualizarClientes;
        }

        public void ActualizarClientes(List<string[]> archivo)
        {
            foreach (var cliente in archivo)
            {
                try
                {
                    ClsDataBase connex = new ClsDataBase();
                    SqlConnection conexion = connex.Conexion();
                    SqlCommand cmd = new SqlCommand("SpClientes", conexion);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Metodo", 3);
                    cmd.Parameters.AddWithValue("@Cedula", cliente[0]);
                    cmd.Parameters.AddWithValue("@Nombre", cliente[1]);
                    cmd.Parameters.AddWithValue("@Apellido", cliente[2]);
                    cmd.Parameters.AddWithValue("@Edad", cliente[3]);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                }
                catch (Exception ex)
                {
                    novedades++;
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        public void ActualizarLog(int nuevos, int actualizados)
        {
            var FechaHora = DateTime.Now.ToString().Split(' ');
            var Log = "[" +FechaHora[0]+" , " + FechaHora[1]+ "] = "+
                "registros ingresados: " + nuevos + ", registros actualizados: "+actualizados + ", registros con novedades: " + novedades;
            StreamWriter file = new StreamWriter("C:\\Users\\DELL\\Documents\\repo-testing\\Servicio Windows\\ActualizadorArchivos\\log.txt", true);
            file.WriteLine(Log); 
            file.Close();
        }
    }
}
