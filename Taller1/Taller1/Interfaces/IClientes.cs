using Taller1.Models;

namespace Taller1.Interfaces
{
    public interface IClientes
    {
        Task<int> CreateCliente(Cliente cliente);
        Task<Cliente> ReadCliente(string id);
        Task<int> UpdateCliente(string id, Cliente cliente);
        Task<int> DeleteCliente(string id);
    }
}
