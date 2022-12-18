using API2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        // GET: api/<ProductosController>
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            var lista = new List<Producto>();
            try
            {
                SqlConnection dataBase = new("Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;Initial Catalog=Prueba;Integrated Security=True");
                SqlCommand cmd = new("SpProducto", dataBase);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 1);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                dataBase.Open();
                da.Fill(dt);
                dataBase.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    lista.Add(new()
                    {
                        Id = int.Parse(dr["Codigo"].ToString()),
                        Nombre = dr["Nombre"].ToString(),
                        Descripcion = dr["Descripcion"].ToString(),
                        Precio = decimal.Parse(dr["Precio"].ToString())
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return lista;
        }

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductosController>
        [HttpPost]
        [Route("GuardarProducto")]
        public void Post([FromBody] Producto value)
        {
            try
            {
                SqlConnection dataBase = new("Data Source=DESKTOP-KOV2VNC\\SQLEXPRESS;Initial Catalog=Prueba;Integrated Security=True");
                SqlCommand cmd = new("SpProducto", dataBase);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Metodo", 4);
                cmd.Parameters.AddWithValue("@codigo", value.Id);
                cmd.Parameters.AddWithValue("@nombre", value.Nombre);
                cmd.Parameters.AddWithValue("@descripcion", value.Descripcion);
                cmd.Parameters.AddWithValue("@precio", value.Precio);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                dataBase.Open();
                da.Fill(dt);
                dataBase.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT api/<ProductosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
