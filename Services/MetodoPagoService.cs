using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace CarRental.Services
{
    public class MetodoPagoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public MetodoPagoService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<MetodoPago>> ListarMetodosPago()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.MetodoPagos
                .AsNoTracking()
                .Include(mp => mp.Reserva)
                .ToListAsync();
        }

        public async Task<MetodoPago?> ObtenerMetodoPagoPorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.MetodoPagos
                .AsNoTracking()
                .Include(mp => mp.Reserva)
                .FirstOrDefaultAsync(mp => mp.MetodoPagoId == id);
        }

        public async Task<bool> GuardarMetodoPago(MetodoPago metodoPago)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            if (metodoPago.MetodoPagoId == 0) // Nuevo método de pago
            {
                contexto.MetodoPagos.Add(metodoPago);
            }
            else // Modificar método de pago existente
            {
                contexto.MetodoPagos.Update(metodoPago);
            }

            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarMetodoPago(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var metodoPago = await contexto.MetodoPagos.FindAsync(id);

            if (metodoPago != null)
            {
                contexto.MetodoPagos.Remove(metodoPago);
                await contexto.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
