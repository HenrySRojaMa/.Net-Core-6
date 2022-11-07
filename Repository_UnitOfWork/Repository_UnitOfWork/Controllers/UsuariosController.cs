using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_UnitOfWork.Services;

namespace Repository_UnitOfWork.Controllers
{
    public class UsuariosController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsuariosController(IUserService userService)
        {
            _userService = userService;
        }

    }
}
