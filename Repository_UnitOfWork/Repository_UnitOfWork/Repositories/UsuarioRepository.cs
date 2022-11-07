using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FacturaContext context) : base(context)
        {
        }
    }
}
