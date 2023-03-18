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
using System.Text.Json.Serialization;
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

        public async Task<Respuesta> Get(int id)
        {
            Respuesta respuesta;

            //Data
            var calculoDepreciacion =  mapper.Map<CalculoDepreciacionGetDTO>
                            (await DbContext.CalculoDepreciacion.
                            Include(x => x.ActivosFijos).
                            FirstOrDefaultAsync(x => x.Id == id));

            if (calculoDepreciacion == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", calculoDepreciacion);
            }

            return respuesta;
        }

        public async Task<Respuesta> Get()
        {
            Respuesta respuesta;

            //Data
            var calculoDepreciacion = mapper.Map<List<CalculoDepreciacionGetDTO>>
                            (await DbContext.CalculoDepreciacion.
                            Include(x => x.ActivosFijos).
                            ToListAsync());

            if (calculoDepreciacion == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", calculoDepreciacion);
            }

            return respuesta;
        }

        public async Task<Respuesta> GetByActivoFijo(int activoFijoId)
        {
            Respuesta respuesta;
            //Data
            var calculoDepreciacion = mapper.Map<List<CalculoDepreciacionGetDTO>>
                            (await DbContext.CalculoDepreciacion.
                            Include(x => x.ActivosFijos).
                            Where(depreciacion => depreciacion.ActivoFijoId == activoFijoId).
                            ToListAsync());

            //var calculoDepreciaciones = await DbContext.CalculoDepreciacion
            //    .Include(x => x.ActivosFijos)
            //    .Select(x=> new CalculoDepreciacionGetDTO
            //    {
            //        Id = x.Id,
            //        AñoProceso = x.AñoProceso,
            //        MesProceso = x.MesProceso,
            //        ActivoFijoId = x.ActivoFijoId,
            //        DescripcionActivosFijos = x.ActivosFijos.Descripcion,
            //        FechaProceso = x.FechaProceso,
            //        MontoDepreciado = x.MontoDepreciado,
            //        DepreciacionAcumulada = x.DepreciacionAcumulada,
            //        CuentaCompra = x.CuentaCompra,
            //        CuentaDepreciacion = x.CuentaDepreciacion
            //    }).Where(depreciacion => depreciacion.ActivoFijoId == activoFijoId)
            //    .ToListAsync();

            if (calculoDepreciacion == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", calculoDepreciacion);
            }

            return respuesta;
        }

        public async Task<Respuesta> Post(CalculoDepreciacionCreateDTO calculoDepreciacionCreateDTO)
        {
            Respuesta respuesta;

            var activoFijo = await DbContext.ActivosFijo.FirstOrDefaultAsync(activoFijo => activoFijo.Id == calculoDepreciacionCreateDTO.ActivoFijoId);

            if (activoFijo == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
            }
            else
            {
                //Mapping information
                CalculoDepreciacion calculoDepreciacion = mapper.Map<CalculoDepreciacion>(calculoDepreciacionCreateDTO);

                calculoDepreciacion.MontoDepreciado = activoFijo.ValorDepreciacion;
                activoFijo.DepreciacionAcumulada += activoFijo.ValorDepreciacion;

                //Adding information
                DbContext.Add(calculoDepreciacion);
                DbContext.Update(activoFijo);
                await DbContext.SaveChangesAsync();

                //Mapping information to show
                CalculoDepreciacionGetDTO calculoDepreciacionGetDTO = mapper.Map<CalculoDepreciacionGetDTO>(calculoDepreciacion);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "Calculo de depreciacion agregado correctamente.", calculoDepreciacionGetDTO);
            }

            return respuesta;
        }

        public async Task<Respuesta> Put(CalculoDepreciacionUpdateDTO calculoDepreciacionUpdateDTO, int id)
        {
            Respuesta respuesta;

            //Calculo depreciacion
            var calculoDepreciacion = await DbContext.CalculoDepreciacion.FirstOrDefaultAsync(x => x.Id == calculoDepreciacionUpdateDTO.Id);

            //Activo fijo
            var activoFijo = await DbContext.ActivosFijo.FirstOrDefaultAsync(x => x.Id == calculoDepreciacionUpdateDTO.ActivoFijoId);

            if (calculoDepreciacionUpdateDTO.Id != id)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El id proporcionado no coincide con el id del calculo de depreciacion.");
            }
            else if (calculoDepreciacion == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "No se encontró el calculo de depreciación.");
            }
            else if (activoFijo == null)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El activo fijo no existe.");
            }
            else
            {
                //Mapping information
                mapper.Map(calculoDepreciacionUpdateDTO, calculoDepreciacion);

                calculoDepreciacion.MontoDepreciado = activoFijo.ValorDepreciacion;
                activoFijo.DepreciacionAcumulada += activoFijo.ValorDepreciacion;

                //Updating information
                DbContext.Update(activoFijo);
                DbContext.Update(calculoDepreciacion);
                await DbContext.SaveChangesAsync();

                //Mapping information to show the data
                CalculoDepreciacionGetDTO calculoDepreciacionGetDTO = mapper.Map<CalculoDepreciacionGetDTO>(calculoDepreciacion);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Calculo de depreciacion acutlizado correctamente", calculoDepreciacionGetDTO);
            }

            return respuesta;
        }

        public async Task<Respuesta> Delete(int id)
        {
            Respuesta respuesta;

            var calculoDepreciacion = await DbContext.CalculoDepreciacion.FirstOrDefaultAsync(x => x.Id == id);

            if (calculoDepreciacion == null)
            {
                respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
            }
            else
            {
                //Deleting information
                DbContext.Remove(calculoDepreciacion);
                await DbContext.SaveChangesAsync();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Calculo depreciacion borrado correctamente.");
            }

            return respuesta;
        }
    }
}
