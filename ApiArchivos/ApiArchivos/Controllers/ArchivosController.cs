using ApiArchivos.Interfaces;
using ApiArchivos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiArchivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {

        private readonly IArchivo _archivo;

        public ArchivosController(IArchivo archivo)
        {
            _archivo = archivo;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> SubirArchivo(IFormFile file)
        {
            return Ok(await _archivo.SubirArchivo(file));
        }

        [HttpGet]
        public async Task<ActionResult> DescargarArchivo(int id)
        {
            var rpta = await _archivo.DescargarArchivo(id);
            if (rpta.Objeto!=null)
            {
                var archivo = (Archivo)rpta.Objeto;
                return File(archivo.FileBytes,archivo.ContentType,archivo.FileName);
            }
            else
            {
                return BadRequest(rpta);
            }
            
        }
    }
}
