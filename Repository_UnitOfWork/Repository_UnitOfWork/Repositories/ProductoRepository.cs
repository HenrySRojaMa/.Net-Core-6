using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Repositories
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(FacturaContext context) : base(context)
        {
        }
    }
}
