using ApiArchivos.Models;
using System.Threading.Tasks;

namespace ApiArchivos.Interfaces
{
    public interface IUser
    {
        Task<Response> RegistrarUsuario(User user);
        Task<Response> ConsultarUsuario(User user);
    }
}
