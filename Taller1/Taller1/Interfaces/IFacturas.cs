using Taller1.Models;

namespace Taller1.Interfaces
{
    public interface IFacturas
    {
        Task<int> CreateFactura(Cabecera cabecera);
        Task<Cabecera> GetFactura(int id);
    }
}
