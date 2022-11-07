using Repository_UnitOfWork.Interfaces;
using Repository_UnitOfWork.Models;

namespace Repository_UnitOfWork.Repositories
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {

        private readonly FacturaContext _context;
        private ICabeceraRepository _cabecera;
        private IClienteRepository _cliente;
        private IProductoRepository _producto;
        private IUsuarioRepository _usuario;

        public UnitOfWork(FacturaContext context)
        {
            _context = context;
        }

        public ICabeceraRepository Cabecera
        {
            get
            {
                if (_cabecera==null)
                {
                    _cabecera = new CabeceraRepository(_context);
                }
                return _cabecera;
            }
        }

        public IClienteRepository Cliente
        {
            get
            {
                if (_cliente == null)
                {
                    _cliente = new ClienteRepository(_context);
                }
                return _cliente;
            }
        }

        public IProductoRepository Producto
        {
            get
            {
                if (_producto == null)
                {
                    _producto = new ProductoRepository(_context);
                }
                return _producto;
            }
        }

        public IUsuarioRepository Usuario
        {
            get
            {
                if (_usuario == null)
                {
                    _usuario = new UsuarioRepository(_context);
                }
                return _usuario;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
