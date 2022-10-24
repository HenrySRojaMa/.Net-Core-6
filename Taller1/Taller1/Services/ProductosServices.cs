using Microsoft.EntityFrameworkCore;
using Taller1.Interfaces;
using Taller1.Models;

namespace Taller1.Services
{
    public class ProductosServices : IProductos
    {

        private readonly PruebaContext _context;

        public ProductosServices(PruebaContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Producto> ReadProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            return producto;

        }

        public async Task<int> UpdateProducto(int id, Producto producto)
        {
            if (id != producto.Codigo)
            {
                return 0;
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return 0;
            }

            _context.Productos.Remove(producto);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Producto>> GetAllProducts()
        {
            List<Producto> products = new List<Producto>();
            try
            {
                products = await _context.Productos.ToListAsync();
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
