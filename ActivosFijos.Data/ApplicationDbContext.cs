using ActivosFijos.Model;
using Microsoft.EntityFrameworkCore;

namespace ActivosFijos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<TipoActivo> TipoActivo { get; set; }
        public DbSet<ActivoFijo> ActivosFijo { get; set; }
        public DbSet<CalculoDepreciacion> CalculoDepreciacion { get; set; }
        public DbSet<AsientosContables> AsientosContables { get; set; }
        public DbSet<User> User { get; set; }
    }
}