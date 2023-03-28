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
    public class ConsultasService<T> : IConsultasService<T>
    {
        private readonly ApplicationDbContext DbContext;


        public ConsultasService(ApplicationDbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public async Task<Respuesta> Get(int? idTipoActivo, int? idDepartamento, DateTime? fechaRegistroDesde, 
            DateTime? fechaRegistroHasta, int? anioDepreaciacionDesde, int? anioDepreciacionHasta, 
            double? depreciacionDesde, double? depreciacionHasta, bool? depreciacionRealizada)
        {
            Respuesta respuesta;

            var consulta = DbContext.ActivosFijo.
                Include(x=> x.CalculoDepreciaciones).
                AsQueryable();

            //Id del tipo activo
            if (idTipoActivo.HasValue)
            {
                consulta = consulta.Where(x => x.TipoActivoId == idTipoActivo);
            }

            //Id del departamento
            if (idDepartamento.HasValue)
            {
                consulta = consulta.Where(x => x.DepartamentoId == idDepartamento);
            }

            //Fecha de registro
            if (fechaRegistroDesde.HasValue && fechaRegistroHasta.HasValue)
            {
                consulta = consulta.Where(x => x.FechaRegistro >= fechaRegistroDesde && x.FechaRegistro <= fechaRegistroHasta);
            }

            //Año de depreciación
            if (anioDepreaciacionDesde.HasValue && anioDepreciacionHasta.HasValue)
            {
                consulta = consulta.Where(x => x.AnioDepreciacion >= anioDepreaciacionDesde && x.AnioDepreciacion <= anioDepreciacionHasta);
            }

            //Depreciación acumulada
            if (depreciacionDesde.HasValue && depreciacionHasta.HasValue)
            {
                consulta = consulta.Where(x => x.DepreciacionAcumulada >= depreciacionDesde && x.DepreciacionAcumulada <= depreciacionHasta);
            }

            //Depreciación realizada
            if (depreciacionRealizada.HasValue && depreciacionRealizada == true)
            {
                consulta = consulta.Where(x => x.DepreciacionAcumulada > 0);
            }

            //Depreciación realizada
            if (depreciacionRealizada.HasValue && depreciacionRealizada == false)
            {
                consulta = consulta.Where(x => x.DepreciacionAcumulada == 0);
            }

            //Transformando la consulta a un query
            var query = await consulta.ToListAsync();

            //Verificando si hay datos
            if (query.Count > 0)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", query);
            }
            else
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Sin registros para mostrar.", query);
            }

            return respuesta;
        }
    }
}
