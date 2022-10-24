using Taller1.Interfaces;
using Taller1.Models;

namespace Taller1.Services
{
    public class FacturasSrevices:IFacturas
    {
        private readonly PruebaContext _context;

        public FacturasSrevices(PruebaContext context)
        {
            _context = context;
        }

        public async Task<int> CreateFactura(Cabecera cabecera)
        {
            _context.Cabeceras.Add(cabecera);
            foreach (var item in cabecera.Detalles)
            {
                _context.Detalles.Add(item);
            }
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            //return Task.FromResult(0);
        }

        public Task<Cabecera> GetFactura(int id)
        {
            throw new NotImplementedException();
        }
    }
}
