using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;
using Repository_UnitOfWork.Utilities;

namespace Repository_UnitOfWork.Services
{
    public class UserService:IUserService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<Usuario> passwordHasher)
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public Task<string> RegisterAsync(Cliente cliente, Usuario usuario)
        {
            usuario.Contrasenia = _passwordHasher.HashPassword(usuario,usuario.Contrasenia);
            var usuarioExiste = _unitOfWork.Usuario.Find(u=>u.Id==usuario.Id).FirstOrDefault();
            if (usuarioExiste == null)
            {

            }
            else
            {

            }
            return null;
        }
    }
}
