using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
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
    public class TipoActivoService<T> : ITipoActivoService<T>
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public TipoActivoService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<TipoActivoGetDTO> Get(int id)
        {
            var tipoActivo = await DbContext.TipoActivo.Select(x=> new TipoActivoGetDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                CuentaContableCompra = x.CuentaContableCompra,
                CuentaContableDepreciacion = x.CuentaContableDepreciacion,
                Estado = Utilities.Estado(x.Estado),
                ActivosFijos = x.ActivosFijos
            }).
            Where(x=> x.Id == id).
            FirstOrDefaultAsync();

            return tipoActivo;
        }

        public async Task<List<TipoActivoGetDTO>> Get()
        {
            var tipoActivos = await DbContext.TipoActivo.Select(x => new TipoActivoGetDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                CuentaContableCompra = x.CuentaContableCompra,
                CuentaContableDepreciacion = x.CuentaContableDepreciacion,
                Estado = Utilities.Estado(x.Estado),
                ActivosFijos = x.ActivosFijos
            }).ToListAsync();

            return tipoActivos;
        }

        public async Task<TipoActivo> Post(TipoActivoCreateDTO tipoActivoCreateDTO)
        {
            //Mapping information
            TipoActivo tipoActivo = mapper.Map<TipoActivo>(tipoActivoCreateDTO);

            //Adding information
            DbContext.Add(tipoActivo);
            await DbContext.SaveChangesAsync();

            return tipoActivo;
        }

        public async Task Put(TipoActivoUpdateDTO tipoActivoUpdateDTO, TipoActivoGetDTO tipoActivoGetDTO)
        {
            //Mapping information
            mapper.Map(tipoActivoUpdateDTO, tipoActivoGetDTO);

            //Updating information
            DbContext.Entry(tipoActivoGetDTO).State= EntityState.Modified;
            DbContext.Update(tipoActivoGetDTO);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            //Deleting information
            DbContext.Remove(new TipoActivo { Id = id});
            await DbContext.SaveChangesAsync();
        }
        public async Task Delete(TipoActivoGetDTO tipoActivoGetDTO)
        {
            //Deleting information
            DbContext.Remove(tipoActivoGetDTO);
            await DbContext.SaveChangesAsync();
        }
    }
}
