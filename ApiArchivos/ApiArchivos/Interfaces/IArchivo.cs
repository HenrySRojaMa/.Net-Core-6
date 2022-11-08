using ApiArchivos.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ApiArchivos.Interfaces
{
    public interface IArchivo
    {
        Task<Response> SubirArchivo(IFormFile file);
        Task<Response> DescargarArchivo(int Id);
    }
}
