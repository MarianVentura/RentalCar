using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalCar.Models;

namespace RentalCar.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<Vehiculos> Vehiculos { get; set; }
    }
}
