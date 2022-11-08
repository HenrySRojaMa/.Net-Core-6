using ApiArchivos.Interfaces;
using ApiArchivos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiArchivos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> RegistrarUsuario(User user)
        {
            return Ok(await _user.RegistrarUsuario(user));
        }

        [HttpGet]
        public async Task<ActionResult<Response>> VerificarUsuario([FromQuery]User user)
        {
            return Ok(await _user.ConsultarUsuario(user));
        }
    }
}
