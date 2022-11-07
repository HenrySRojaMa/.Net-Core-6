using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(Cliente cliente,Usuario usuario);
    }
}
