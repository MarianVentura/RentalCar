using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Services
{
    public class ReservasService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ReservasService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Reservas>> ListarReservas()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .ToListAsync();
        }

        public async Task<Reservas?> ObtenerReservaPorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Reservas
                .AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.Vehiculo)
                .FirstOrDefaultAsync(r => r.ReservaId == id);
        }

        public async Task<bool> GuardarReserva(Reservas reserva)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            if (reserva.ReservaId == 0) // Nueva reserva
            {
                contexto.Reservas.Add(reserva);
            }
            else // Modificar reserva existente
            {
                contexto.Reservas.Update(reserva);
            }

            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarReserva(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var reserva = await contexto.Reservas.FindAsync(id);

            if (reserva != null)
            {
                contexto.Reservas.Remove(reserva);
                await contexto.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
