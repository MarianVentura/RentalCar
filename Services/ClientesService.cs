using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Services
{
    public class ClientesService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public ClientesService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<List<Clientes>> ListarClientes()
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Clientes?> ObtenerClientePorId(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            return await contexto.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);
        }

        public async Task<bool> GuardarCliente(Clientes cliente)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();

            if (cliente.ClienteId == 0) // Nuevo cliente
            {
                contexto.Clientes.Add(cliente);
            }
            else // Modificar cliente existente
            {
                contexto.Clientes.Update(cliente);
            }

            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            await using var contexto = await _dbFactory.CreateDbContextAsync();
            var cliente = await contexto.Clientes.FindAsync(id);

            if (cliente != null)
            {
                contexto.Clientes.Remove(cliente);
                await contexto.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
