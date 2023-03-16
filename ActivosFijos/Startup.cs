using ActivosFijos.Data;
using ActivosFijos.Data.Interfaces;
using ActivosFijos.Data.Interfaces.Services;
using ActivosFijos.Mapper;
using ActivosFijos.Model.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ActivosFijos
{
    public class Startup
    {

        private readonly string Cors = "Cors";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Avoiding cyclic reference 
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //Adding dbcontext
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Scopes
            services.AddScoped<IDepartamentoService<Departamento>, DepartamentoService<Departamento>>();
            services.AddScoped<IEmpleadoService<Empleado>, EmpleadoService<Empleado>>();
            services.AddScoped<ITipoActivoService<TipoActivo>, TipoActivoService<TipoActivo>>();
            services.AddScoped<IActivoFijoService<ActivoFijo>, ActivoFijoService<ActivoFijo>>();
            services.AddScoped<ICalculoDepreciacionService<CalculoDepreciacion>, CalculoDepreciacionService<CalculoDepreciacion>>();

            //Mapper
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: Cors, builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Cors
            app.UseCors(Cors);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
