using Microsoft.EntityFrameworkCore;
using Taller1.Interfaces;
using Taller1.Models;

namespace Taller1.Services
{
    public class ClientesService : IClientes
    {

        private readonly PruebaContext _context;

        public ClientesService(PruebaContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Cliente> ReadCliente(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            return cliente;
        }

        public async Task<int> UpdateCliente(string id, Cliente cliente)
        {
            if (id != cliente.Cedula)
            {
                return 0;
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteCliente(string id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return 0;
            }

            _context.Clientes.Remove(cliente);
            return await _context.SaveChangesAsync();

        }

    }
}
