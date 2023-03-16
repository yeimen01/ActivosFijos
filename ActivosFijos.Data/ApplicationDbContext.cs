using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
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

            //Usuario
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin1234"
            });

            //Empleado
            modelBuilder.Entity<Empleado>().HasData(new Empleado
            {
                Id = 1,
                Nombre = "Carlos",
                Apellido = "Gomez",
                Cedula = "40213108481",
                DepartamentoId = 1,
                TipoPersona = 0,
                FechaIngreso = DateTime.Now
            });

            //Tipo activo
            modelBuilder.Entity<TipoActivo>().HasData(new TipoActivo
            {
                Id = 1,
                Descripcion = "Electronico",
                CuentaContableCompra = "65",
                CuentaContableDepreciacion = "66", 
                Estado = 0
            });

            //Activo fijo
            modelBuilder.Entity<ActivoFijo>().HasData(new ActivoFijo
            {
                Id = 1,
                Descripcion = "Laptop",
                DepartamentoId = 1,
                TipoActivoId = 1,
                FechaRegistro  = DateTime.Now,
                ValorCompra = 25000,
                ValorDepreciacion = 4000,
                DepreciacionAcumulada = 0,
                AnioDepreciacion = 2025
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