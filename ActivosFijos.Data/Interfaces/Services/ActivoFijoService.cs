using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ActivoFijoGetDTO> Get(int id)
        {
            var activoFijo = await DbContext.ActivosFijo.Select(x => new ActivoFijoGetDTO
            {
                Id = x.Id,
                Descripcion  = x.Descripcion,
                DepartamentoId = x.DepartamentoId,
                DescripcionDepartamento = x.Departamento.Descripcion,
                TipoActivoId = x.TipoActivoId,
                DescripcionTipoActivo = x.TipoActivo.Descripcion,
                FechaRegistro = x.FechaRegistro, 
                ValorCompra = x.ValorCompra,
                ValorDepreciacion = x.ValorDepreciacion,
                DepreciacionAcumulada = x.DepreciacionAcumulada
                }).Where(x=> x.Id == id).FirstOrDefaultAsync();

            return activoFijo;
        }

        public async Task<List<ActivoFijoGetDTO>> Get()
        {
            var activosFijos = await DbContext.ActivosFijo.Select(x => new ActivoFijoGetDTO
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                DepartamentoId = x.DepartamentoId,
                DescripcionDepartamento = x.Departamento.Descripcion,
                TipoActivoId = x.TipoActivoId,
                DescripcionTipoActivo = x.TipoActivo.Descripcion,
                FechaRegistro = x.FechaRegistro,
                ValorCompra = x.ValorCompra,
                ValorDepreciacion = x.ValorDepreciacion,
                DepreciacionAcumulada = x.DepreciacionAcumulada
            }).ToListAsync(); 

            return activosFijos;
        }

        public async Task<ActivoFijo> Post(ActivoFijoCreateDTO activoFijoDTO)
        {
            //Mapping information
            ActivoFijo activoFijo = mapper.Map<ActivoFijo>(activoFijoDTO);

            //Saving information
            DbContext.Add(activoFijo);
            await DbContext.SaveChangesAsync();

            return activoFijo;
        }

        public async Task Put(ActivoFijoUpdateDTO activoFijoDTO, ActivoFijoGetDTO activoFijo)
        {
            //Mapping information
            mapper.Map(activoFijoDTO, activoFijo);

            //Updating information
            DbContext.Entry(activoFijo).State = EntityState.Modified;
            DbContext.Update(activoFijo);
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            //Deleting information
            DbContext.Remove(new ActivoFijo() { Id = id });
            await DbContext.SaveChangesAsync();
        }
        public async Task Delete(ActivoFijoGetDTO activofijo)
        {
            //Deleting information
            DbContext.Remove(activofijo);
            await DbContext.SaveChangesAsync();
        }
    }
}
