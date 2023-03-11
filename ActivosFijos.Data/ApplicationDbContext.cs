using ActivosFijos.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivosFijos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base (options)
        {

        }

        //Los roles del sistema
        private readonly string[] Departamentos =
        {
            "Administracion",
            "Contabilidad",
            "Informatica"
        };

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int id = 1;
            foreach (string departamento in Departamentos)
            {
                modelBuilder.Entity<Departamento>().HasData(new Departamento
                {
                    Id = id,
                    Descripcion= departamento,
                });

                base.OnModelCreating(modelBuilder);

                id++;
            }

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin1234"
            });

            base.OnModelCreating(modelBuilder);
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