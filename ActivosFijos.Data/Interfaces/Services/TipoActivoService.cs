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
    public class TipoActivoService<T> : ITipoActivoService<T>
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public TipoActivoService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<Respuesta> Get(int id)
        {
            Respuesta respuesta;

            var tipoActivo = mapper.Map<TipoActivoGetDTO>(
                await DbContext.TipoActivo.
                Include(x=> x.ActivosFijos).
                FirstOrDefaultAsync(x=> x.Id == id));

            if (tipoActivo == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", tipoActivo);
            }

            return respuesta;
        }

        public async Task<Respuesta> Get()
        {
            Respuesta respuesta;

            var tipoActivo = mapper.Map<List<TipoActivoGetDTO>>(
                await DbContext.TipoActivo.
                Include(x => x.ActivosFijos).
                ToListAsync());

            if (tipoActivo == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else if (tipoActivo.Count == 0)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NoContent, "Aún no se ha agregado ningún tipo de activo");
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", tipoActivo);
            }

            return respuesta;
        }

        public async Task<Respuesta> Post(TipoActivoCreateDTO tipoActivoCreateDTO)
        {
            //Mapping information to create the data
            TipoActivo tipoActivo = mapper.Map<TipoActivo>(tipoActivoCreateDTO);

            //Adding information to the context
            DbContext.Add(tipoActivo);
            await DbContext.SaveChangesAsync();

            //Mapping information to show the data
            TipoActivoGetDTO tipoActivoGetDTO = mapper.Map<TipoActivoGetDTO>(tipoActivo);

            //Respuesta
            Respuesta respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Tipo de activo agregado correctamente", tipoActivoGetDTO);

            return respuesta;
        }

        public async Task<Respuesta> Put(TipoActivoUpdateDTO tipoActivoUpdateDTO, int id)
        {
            Respuesta respuesta;

            //Verifying existense
            var tipoActivo = await DbContext.TipoActivo.FirstOrDefaultAsync(x => x.Id == id);

            //Verifying id
            if (tipoActivoUpdateDTO.Id != id)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del tipo de activo.");
            }
            else if (tipoActivo == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            //else if (!Enum.IsDefined(typeof(Estado), tipoActivoUpdateDTO.Estado))
            //{
            //    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El estado suminstrado no existe.");
            //}
            else
            {
                //Mapping information
                mapper.Map(tipoActivoUpdateDTO, tipoActivo);

                //Updating information
                DbContext.Update(tipoActivo);
                await DbContext.SaveChangesAsync();

                //Mapping information to show the data
                TipoActivoGetDTO tipoActivoGetDTO = mapper.Map<TipoActivoGetDTO>(tipoActivo);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Tipo activo actualizado correctamente.", tipoActivoGetDTO);
            }

            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta;

            var existeTipoActivo = await DbContext.TipoActivo.FirstOrDefaultAsync(x=> x.Id == id);

            if (existeTipoActivo == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else if (existeTipoActivo.ActivosFijos != null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El tipo de activo no se puede borrar, ya que posee activos fijos.");
            }
            else
            {
                //Deleting information
                DbContext.Remove(existeTipoActivo);
                await DbContext.SaveChangesAsync();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Tipo de activo borrado correctamente");
            }

            return respuesta;
        }
    }
}
