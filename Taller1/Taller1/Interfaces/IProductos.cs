using Taller1.Models;

namespace Taller1.Interfaces
{
    public interface IProductos
    {
        Task<int> CreateProducto(Producto producto);
        Task<Producto> ReadProducto(int id);
        Task<int> UpdateProducto(int id, Producto producto);
        Task<int> DeleteProducto(int id);
        Task<List<Producto>> GetAllProducts();
    }
}
