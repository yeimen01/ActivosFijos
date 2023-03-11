using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
using ActivosFijos.Model.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces.Services
{
    public class EmpleadoService<T> : IEmpleadoService<T>
    {
        public ApplicationDbContext DbContext { get; }
        public IMapper mapper { get; }

        public EmpleadoService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<EmpleadoGetDTO> Get(int id)
        {
            var empleado = await DbContext.Empleado.
                Select(x => new EmpleadoGetDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Cedula = x.Cedula,
                    DepartamentoId = x.DepartamentoId,
                    DescripcionDepartamento = x.Departamento.Descripcion,
                    TipoPersona = Utilities.TipoPersona(x.TipoPersona),
                    FechaIngreso = x.FechaIngreso,
                    Estado = Utilities.Estado(x.Estado)
                }).Where(x=> x.Id == id).
                FirstOrDefaultAsync();

            return empleado;
        }

        public async Task<List<EmpleadoGetDTO>> Get()
        {
            var empleados = await DbContext.Empleado.Select(x => new EmpleadoGetDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Cedula = x.Cedula,
                DepartamentoId = x.DepartamentoId,
                DescripcionDepartamento = x.Departamento.Descripcion,
                TipoPersona = Utilities.TipoPersona(x.TipoPersona),
                FechaIngreso = x.FechaIngreso,
                Estado = Utilities.Estado(x.Estado)
            }).ToListAsync();

            return empleados;
        }

        public async Task<Empleado> Post(EmpleadoCreateDTO empleadoDTO)
        {
            //Mapping information
            Empleado empleado = mapper.Map<Empleado>(empleadoDTO);

            //Adding the information
            DbContext.Add(empleado);
            await DbContext.SaveChangesAsync();

            return empleado;
        }

        public async Task Put(EmpleadoUpdateDTO empleadoDTO, EmpleadoGetDTO empleado)
        {
            //Mapping information
            mapper.Map(empleadoDTO, empleado);

            //Updating information
            DbContext.Entry(empleado).State = EntityState.Modified;
            DbContext.Update(empleado);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            //Deleting information
            DbContext.Remove(new Empleado() { Id = id });
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(EmpleadoGetDTO empleado)
        {
            //Deleting information
            DbContext.Remove(empleado);
            await DbContext.SaveChangesAsync();
        }
    }
}
