using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
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
    public class ActivoFijoService<T> : IActivoFijoService<T>
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public ActivoFijoService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<Respuesta> Get(int id)
        {
            Respuesta respuesta;

            var activoFijo = mapper.Map<ActivoFijoGetDTO>(
                await DbContext.ActivosFijo.
                Include(x => x.Departamento).
                Include(x => x.TipoActivo).
                Include(x => x.CalculoDepreciaciones).
                FirstOrDefaultAsync(x => x.Id == id)
                );

            if (activoFijo == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", activoFijo);
            }

            return respuesta;
        }
        public async Task<Respuesta> Get()
        {
            Respuesta respuesta;

            var activosFijos = mapper.Map<List<ActivoFijoGetDTO>>(
                await DbContext.ActivosFijo.
                Include(x => x.Departamento).
                Include(x => x.TipoActivo).
                Include(x => x.CalculoDepreciaciones).
                ToListAsync());

            if (activosFijos == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", activosFijos);
            }

            return respuesta;
        }
        public async Task<Respuesta> Post(ActivoFijoCreateDTO activoFijoreateDTO)
        {
            Respuesta respuesta;

            //Departamento
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=> x.Id == activoFijoreateDTO.DepartamentoId);

            //Tipo activo
            var tipoActivo = await DbContext.TipoActivo.FirstOrDefaultAsync(x=> x.Id == activoFijoreateDTO.TipoActivoId);

            if (departamento == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotró el departamento.");
            }
            else if (tipoActivo == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotró el tipo de activo.");
            }
            else if (activoFijoreateDTO.AnioDepreciacion < DateTime.Now.Year)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El año de depreciación no puede ser menor al año en curso.");
            }
            else
            {
                //Mapping information
                ActivoFijo activoFijo = mapper.Map<ActivoFijo>(activoFijoreateDTO);

                //Saving information
                DbContext.Add(activoFijo);
                await DbContext.SaveChangesAsync();

                //Mapping information to show the data
                ActivoFijoGetDTO activoFijoGetDTO = mapper.Map<ActivoFijoGetDTO>(activoFijo);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Activo fijo agregado correctamente.", activoFijoGetDTO);
            }

            return respuesta;
        }
        public async Task<Respuesta> Put(ActivoFijoUpdateDTO activoFijoUpdateDTO, int id)
        {
            Respuesta respuesta;

            //Activo fijo
            var activoFijo = await DbContext.ActivosFijo.FirstOrDefaultAsync(x=> x.Id == id);

            //Departamento
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x=> x.Id == activoFijoUpdateDTO.DepartamentoId);

            //Tipo activo
            var tipoActivo = await DbContext.TipoActivo.FirstOrDefaultAsync(x=> x.Id == activoFijoUpdateDTO.TipoActivoId);

            if (activoFijoUpdateDTO.Id != id)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El id proporcionado no coincide con el id del activo fijo.");
            } 
            else if (activoFijo == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else if (departamento == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotró el departamento.");
            }
            else if (tipoActivo == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotro el tipo de activo.");
            }
            else
            {
                //Mapping information
                mapper.Map(activoFijoUpdateDTO, activoFijo);

                //Updating information
                DbContext.Update(activoFijo);
                await DbContext.SaveChangesAsync();

                //Mapping information to show the data
                ActivoFijoGetDTO activoFijoGetDTO = mapper.Map<ActivoFijoGetDTO>(activoFijo);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Activo fijo actualizado correctamente.", activoFijoGetDTO);
            }

            return respuesta;
        }
        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta;

            var activoFijo = await DbContext.ActivosFijo.FirstOrDefaultAsync(x=> x.Id == id);

            if (activoFijo == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Deleting information
                DbContext.Remove(activoFijo.CalculoDepreciaciones);
                DbContext.Remove(activoFijo);
                await DbContext.SaveChangesAsync();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Activo fijo borrado correctamente.");
            }

            return respuesta;
        }
    }
}
