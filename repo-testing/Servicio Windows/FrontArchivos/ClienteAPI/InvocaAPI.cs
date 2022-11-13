using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Data;
using Newtonsoft.Json;

namespace ClienteAPI
{
    public class InvocaAPI
    {
        string urlBase = "https://localhost:44360/api/";
        public int ActualizarCliente(Cliente c)
        {
            int rpta = 0;
            try
            {
                string urlServicio = urlBase + "cliente/actualizar";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    rpta = int.Parse(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                return rpta;
            }
            return rpta;
        }
        public List<Cliente> ListarClientes()
        {
            List<Cliente> clientes = null;
            string urlServicio = urlBase + "cliente/listar";
            HttpClient clienteHttpExterno = new HttpClient();
            HttpResponseMessage response = clienteHttpExterno.GetAsync(urlServicio).Result;
            if (response.IsSuccessStatusCode)
            {
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                clientes = null;
            }
            return clientes;
        }
        public List<Cliente> FiltrarClientes(Cliente c)
        {
            List<Cliente> clientes = null;
            try
            {
                string urlServicio = urlBase + "cliente/filtrar";
                HttpClient clienteHttpExterno = new HttpClient();
                HttpResponseMessage response = clienteHttpExterno.PostAsync(urlServicio, new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    clientes = null;
                }
                return clientes;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
