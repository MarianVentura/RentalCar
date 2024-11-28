using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Services
{
    public class VehiculosService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public VehiculosService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Vehiculos>> ListarVehiculos()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Vehiculos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Vehiculos?> ObtenerVehiculoPorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Vehiculos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VehiculosId == id);
        }

        public async Task<bool> GuardarVehiculo(Vehiculos vehiculo)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            if (vehiculo.VehiculosId == 0) // Nuevo vehículo
            {
                contexto.Vehiculos.Add(vehiculo);
            }
            else // Modificar vehículo existente
            {
                contexto.Vehiculos.Update(vehiculo);
            }

            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVehiculo(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var vehiculo = await contexto.Vehiculos.FindAsync(id);

            if (vehiculo != null)
            {
                contexto.Vehiculos.Remove(vehiculo);
                await contexto.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
