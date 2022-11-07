using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Interfaces
{
    public interface IClienteRepository:IGenericRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> GetClientesSinCorreo2();
    }
}
