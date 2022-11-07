using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Repositories
{
    public class CabeceraRepository : GenericRepository<Cabecera>, ICabeceraRepository
    {
        public CabeceraRepository(FacturaContext context) : base(context)
        {
        }
    }
}
