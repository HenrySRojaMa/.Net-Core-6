using Microsoft.EntityFrameworkCore;
using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(FacturaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetClientesSinCorreo2()
        {
            return await _context.Clientes.Where(c => c.Direccion2 == "").ToListAsync();
        }

        public override async Task<(int totalRegistros, IEnumerable<Cliente> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            var consulta = _context.Clientes as IQueryable<Cliente>;
            if (!String.IsNullOrEmpty(search))
            {
                consulta =consulta.Where(c=>c.Nombre.ToLower().Contains(search));
            }
            var totalRegistros = await consulta.CountAsync();
            var registros = await consulta.
                Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (totalRegistros, registros);
        }

    }
}
