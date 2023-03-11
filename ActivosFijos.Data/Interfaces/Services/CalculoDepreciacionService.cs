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
    public class CalculoDepreciacionService<T> : ICalculoDepreciacionService<T>
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public CalculoDepreciacionService(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        public async Task<CalculoDepreciacionGetDTO> Get(int id)
        {
            var calculoDepreciacion = await DbContext.CalculoDepreciacion.
                Select(x => new CalculoDepreciacionGetDTO
            {
                Id = x.Id,
                AñoProceso = x.AñoProceso,
                MesProceso = x.MesProceso,
                ActivoFijoId = x.ActivoFijoId,
                DescripcionActivosFijos = x.ActivosFijos.Descripcion,
                FechaProceso = x.FechaProceso,
                MontoDepreciado = x.MontoDepreciado,
                DepreciacionAcumulada = x.DepreciacionAcumulada,
                CuentaCompra = x.CuentaCompra,
                CuentaDepreciacion = x.CuentaDepreciacion
            }).
            Where(x => x.Id == id).
            FirstOrDefaultAsync();

            return calculoDepreciacion;
        }

        public async Task<List<CalculoDepreciacionGetDTO>> Get()
        {
            var calculoDepreciaciones = await DbContext.CalculoDepreciacion.
                Select(x => new CalculoDepreciacionGetDTO
                {
                    Id = x.Id,
                    AñoProceso = x.AñoProceso,
                    MesProceso = x.MesProceso,
                    ActivoFijoId = x.ActivoFijoId,
                    DescripcionActivosFijos = x.ActivosFijos.Descripcion,
                    FechaProceso = x.FechaProceso,
                    MontoDepreciado = x.MontoDepreciado,
                    DepreciacionAcumulada = x.DepreciacionAcumulada,
                    CuentaCompra = x.CuentaCompra,
                    CuentaDepreciacion = x.CuentaDepreciacion
                }).ToListAsync();

            return calculoDepreciaciones;
        }

        public async Task<CalculoDepreciacion> Post(CalculoDepreciacionCreateDTO calculoDepreciacionCreateDTO)
        {
            //Mapping information
            CalculoDepreciacion calculoDepreciacion = mapper.Map<CalculoDepreciacion>(calculoDepreciacionCreateDTO);

            //Adding information
            DbContext.Add(calculoDepreciacion);
            await DbContext.SaveChangesAsync();

            return calculoDepreciacion;
        }

        public async Task Put(CalculoDepreciacionUpdateDTO calculoDepreciacionUpdateDTO, CalculoDepreciacionGetDTO calculoDepreciacionGetDTO)
        {
            //Mapping information
            mapper.Map(calculoDepreciacionUpdateDTO, calculoDepreciacionGetDTO);

            //Updating information
            DbContext.Entry(calculoDepreciacionUpdateDTO).State = EntityState.Modified;
            DbContext.Update(calculoDepreciacionUpdateDTO);
            await DbContext.SaveChangesAsync(); 
        }

        public async Task Delete(int id)
        {
            //Deleting information
            DbContext.Remove(new CalculoDepreciacion { Id = id });
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(CalculoDepreciacionGetDTO calculoDepreciacionGetDTO)
        {
            //Deleting information
            DbContext.Remove(calculoDepreciacionGetDTO);
            await DbContext.SaveChangesAsync();
        }
    }
}
