using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
using ActivosFijos.Model.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<Respuesta> Get(int id)
        {
            Respuesta respuesta;

            //Data
            var empleado = await DbContext.Empleado.
                Select(x => new EmpleadoGetDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    Cedula = x.Cedula,
                    DepartamentoId = x.DepartamentoId,
                    DepartamentoDescripcion = x.Departamento.Descripcion,
                    TipoPersona = Utilities.TipoPersona(x.TipoPersona),
                    FechaIngreso = x.FechaIngreso,
                    Estado = Utilities.Estado(x.Estado)
                }).FirstOrDefaultAsync(x => x.Id == id);

            //var empleado = mapper.Map<EmpleadoGetDTO>
            //                (await DbContext.Empleado.
            //                Include(x => x.Departamento).
            //                FirstOrDefaultAsync(x => x.Id == id));

            if (empleado == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", empleado);
            }

            

            return respuesta;
        }

        public async Task<Respuesta> Get()
        {
            Respuesta respuesta;

            var empleados = await DbContext.Empleado.Select(x => new EmpleadoGetDTO
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                Cedula = x.Cedula,
                DepartamentoId = x.DepartamentoId,
                DepartamentoDescripcion = x.Departamento.Descripcion,
                TipoPersona = Utilities.TipoPersona(x.TipoPersona),
                FechaIngreso = x.FechaIngreso,
                Estado = Utilities.Estado(x.Estado)
            }).ToListAsync();

            //var empleados = await DbContext.Empleado.
            //    Include(x=> x.Departamento).
            //    ToListAsync();
            //var empleadosGetDTO = mapper.Map<List<EmpleadoGetDTO>>(empleados);

            if (empleados == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else if (empleados.Count == 0)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NoContent, "Aún no se ha agregado ningún empleado");
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", empleados);
            }

            return respuesta;
        }

        public async Task<Respuesta> Post(EmpleadoCreateDTO empleadoDTO)
        {
            Respuesta respuesta;

            //Verifying the exitanse of the department
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=> x.Id == empleadoDTO.DepartamentoId);

            if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el departamento del empleado que desea agregar.");
            }
            //Verifying tipo persona
            else if (!Enum.IsDefined(typeof(TipoPersona), empleadoDTO.TipoPersona))
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El tipo de persona suministrado no existe.");
            }
            else
            {
                //Mapping information
                Empleado empleado = mapper.Map<Empleado>(empleadoDTO);

                //Adding the information
                DbContext.Add(empleado);
                await DbContext.SaveChangesAsync();

                //Mapping information to show
                EmpleadoGetDTO empleadoGetDTO = mapper.Map<EmpleadoGetDTO>(empleado);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Empleado creado correctamente.", empleadoGetDTO);
            }
            

            return respuesta;
        }

        public async Task<Respuesta> Put(EmpleadoUpdateDTO empleadoDTO, int id)
        {
            Respuesta respuesta;

            //Verifying existense
            var empleado = await DbContext.Empleado.Include(x=> x.Departamento).FirstOrDefaultAsync(x=> x.Id == id);
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=> x.Id == empleadoDTO.DepartamentoId);

            //Verifying id
            if (empleadoDTO.Id != id)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del empleado.");
            }
            else if (empleado == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del empleado que desea actualizar.");
            }
            else if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del departamento que desea actualizar.");
            }
            else if (!Enum.IsDefined(typeof(TipoPersona), empleadoDTO.TipoPersona))
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El tipo de persona suministrado no existe.");
            }
            else if (!Enum.IsDefined(typeof(Estado), empleado.Estado))
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El estado suminstrado no existe.");
            }
            else
            {
                //Mapping information
                mapper.Map(empleadoDTO, empleado);

                //Updating information
                DbContext.Update(empleado);
                await DbContext.SaveChangesAsync();

                //Mapping information
                EmpleadoGetDTO empleadoGetDTO = mapper.Map<EmpleadoGetDTO>(empleado);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Empleado actualizado correctamente", empleadoGetDTO);
            }

            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta;

            //Verifying existense
            var existeEmpleado = await DbContext.Empleado.AnyAsync(x=> x.Id == id);

            if (!existeEmpleado)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del empleado que desea borrar.");
            }
            else
            {
                //Deleting information
                DbContext.Remove(new Empleado() { Id = id });
                await DbContext.SaveChangesAsync();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Departamento borrado correctamente");
            }

            return respuesta;
        }
    }
}
