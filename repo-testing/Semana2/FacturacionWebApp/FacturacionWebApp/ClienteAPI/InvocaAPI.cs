using FacturacionWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace FacturacionWebApp.ClienteAPI
{
    public class InvocaAPI
    {
        string urlBase = System.Configuration.ConfigurationManager.AppSettings["urlAPI"].ToString();
        public ClsTransaccion consultaApi(Usuario requestUsuario)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase+"login/autenticar";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, requestUsuario).Result;
                if (response.IsSuccessStatusCode)
                {
                    rpta = JsonConvert.DeserializeObject<ClsTransaccion>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    rpta.Codigo = "001";
                    rpta.Mensaje = "Error de conexion" + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "001";
                rpta.Mensaje = "Error: " + ex.Message;
            }
            return rpta; 
        }
        public Cliente BuscarClienteLoggeado(string id)
        {
            Cliente cliente = new Cliente();
            string urlServicio = urlBase + "cliente/buscar?id=" + id;
            HttpClient clienteHttpExterno = new HttpClient();
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                cliente = JsonConvert.DeserializeObject<Cliente>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                cliente = null;
            }
            return cliente;
        }

        public ClsTransaccion IngresarProducto(Producto p)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "producto/ingresar";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, p).Result;
                if (response.IsSuccessStatusCode)
                {
                    rpta = JsonConvert.DeserializeObject<ClsTransaccion>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    rpta.Codigo = "001";
                    rpta.Mensaje = "Error de conexion" + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "001";
                rpta.Mensaje = "Error: " + ex.Message;
            }
            return rpta;
        }
        public ClsTransaccion ActualizarProducto(Producto p)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "producto/actualizar";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, p).Result;
                if (response.IsSuccessStatusCode)
                {
                    rpta = JsonConvert.DeserializeObject<ClsTransaccion>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    rpta.Codigo = "001";
                    rpta.Mensaje = "Error de conexion" + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "001";
                rpta.Mensaje = "Error: " + ex.Message;
            }
            return rpta;
        }
        public List<Producto> ListarProductos()
        {
            List<Producto> Productos=null;
            string urlServicio = urlBase + "producto/listar";
            HttpClient clienteHttpExterno = new HttpClient();
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                Productos = JsonConvert.DeserializeObject<List<Producto>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Productos = null;
            }
            return Productos;
        }
        public List<Producto> BuscarProductos(string producto)
        {
            List<Producto> Productos;
            string urlServicio = urlBase + "producto/buscar?producto=" + producto;
            HttpClient clienteHttpExterno = new HttpClient();
            //HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, requestUsuario).Result;
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                Productos = JsonConvert.DeserializeObject<List<Producto>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Productos = null;
            }
            return Productos;
        }


        public TransaccionID IngresarCab(Factura f)
        {
            TransaccionID rpta = new TransaccionID();
            try
            {
                string urlServicio = urlBase + "factura/ingresarcab";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, f).Result;
                if (response.IsSuccessStatusCode)
                {
                    rpta = JsonConvert.DeserializeObject<TransaccionID>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    rpta.IdFactura = 0;
                    rpta.Codigo = "001";
                    rpta.Mensaje = "Error de conexion" + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                rpta.IdFactura = 0;
                rpta.Codigo = "001";
                rpta.Mensaje = "Error: " + ex.Message;
            }
            return rpta;
        }
        public ClsTransaccion IngresarDet(List<Detalle> detalles)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "factura/ingresardet";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, detalles).Result;
                if (response.IsSuccessStatusCode)
                {
                    rpta = JsonConvert.DeserializeObject<ClsTransaccion>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    rpta.Codigo = "001";
                    rpta.Mensaje = "Error de conexion" + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "001";
                rpta.Mensaje = "Error: " + ex.Message;
            }
            return rpta;
        }
        public Factura2 ConsultarFac(int nFactura)
        {
            Factura2 f=null;
            string urlServicio = urlBase + "factura/consultar?codigoFactura=" + nFactura;
            HttpClient clienteHttpExterno = new HttpClient();
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                f = JsonConvert.DeserializeObject<Factura2>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                f = null;
            }
            return f;
        }
    }
}