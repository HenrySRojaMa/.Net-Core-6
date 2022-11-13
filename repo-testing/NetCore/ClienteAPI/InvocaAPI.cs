using Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace ClienteAPI
{
    public class InvocaAPI
    {
        string urlBase = "http://localhost:18672/api/";
        public ClsTransaccion consultaApi(Usuario requestUsuario)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "security/autenticar";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(requestUsuario),Encoding.UTF8, "application/json")).Result;
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

        public Cliente BuscarClienteLoggeado(string token, string id)
        {
            Cliente cliente = new Cliente();
            string urlServicio = urlBase + "cliente/buscar?id=" + id;
            HttpClient clienteHttpExterno = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
            clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
        public ClsTransaccion ActualizarCliente (string token, Cliente c)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "cliente/actualizar";
                HttpClient clienteHttpExterno = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
                clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json")).Result;
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

        public ClsTransaccion IngresarProducto(string token, Producto p)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "producto/ingresar";
                HttpClient clienteHttpExterno = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
                clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json")).Result;
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
        public ClsTransaccion ActualizarProducto(string token, Producto p)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "producto/actualizar";
                HttpClient clienteHttpExterno = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
                clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json")).Result;
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
        public List<Producto> ListarProductos(string token)
        {
            List<Producto> Productos = null;
            string urlServicio = urlBase + "producto/listar";
            HttpClient clienteHttpExterno = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
            clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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
        public List<Producto> BuscarProductos(string token, string producto)
        {
            List<Producto> Productos;
            string urlServicio = urlBase + "producto/buscar?producto=" + producto;
            HttpClient clienteHttpExterno = new HttpClient();
            //HttpResponseMessage response = clienteHttpExterno.PostAsJsonAsync(urlServicio, requestUsuario).Result;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
            clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public TransaccionID IngresarCab(string token, Factura f)
        {
            TransaccionID rpta = new TransaccionID();
            try
            {
                string urlServicio = urlBase + "factura/ingresarcab";
                HttpClient clienteHttpExterno = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
                clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json")).Result;
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
        public ClsTransaccion IngresarDet(string token, List<Detalle> detalles)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                string urlServicio = urlBase + "factura/ingresardet";
                HttpClient clienteHttpExterno = new HttpClient();
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
                clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(detalles), Encoding.UTF8, "application/json")).Result;
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
        public Factura2 ConsultarFac(string token, int nFactura)
        {
            Factura2 f = null;
            string urlServicio = urlBase + "factura/consultar?codigoFactura=" + nFactura;
            HttpClient clienteHttpExterno = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
            clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
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

        public List<Lugar> ListarProvincias(string token)
        {

            List<Lugar> Provincias = null;
            string urlServicio = urlBase + "lugar/provincias";
            HttpClient clienteHttpExterno = new HttpClient();
            //var session = JsonConvert.DeserializeObject<SessionUsuario>(HttpContext.Session.GetString("DatosUsuario"));
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
            clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token /*session.Token*/);
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                Provincias = JsonConvert.DeserializeObject<List<Lugar>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Provincias = null;
            }
            return Provincias;
        }
        public List<Lugar> BuscarCiudades(string token, int CodigoP)
        {
            List<Lugar> Ciudades = null;
            string urlServicio = urlBase + "lugar/ciudades?CodigoP="+CodigoP;
            HttpClient clienteHttpExterno = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            clienteHttpExterno.DefaultRequestHeaders.Accept.Add(contentType);
            clienteHttpExterno.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                Ciudades = JsonConvert.DeserializeObject<List<Lugar>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                Ciudades = null;
            }
            return Ciudades;
        }
    }
}
