using ActivosFijos.Data.Interfaces;
using ActivosFijos.Data.Interfaces.Services;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController
    {
        private readonly IConsultasService<ActivoFijo> _consultasService;

        public ConsultasController(IConsultasService<ActivoFijo> _consultasService)
        {
            this._consultasService = _consultasService;
        }

        [HttpGet]
        public async Task<ObjectResult> Consultas([FromQuery] int? TipoActivo, [FromQuery] int? Departamento, [FromQuery] DateTime? FechaRegistroDesde,
            [FromQuery] DateTime? FechaRegistroHasta, [FromQuery] int? AnioDepreaciacionDesde, [FromQuery] int? AnioDepreciacionHasta, 
            [FromQuery] double? DepreciacionAcumuladaDesde, [FromQuery] double? DepreciacionAcumuladaHasta, [FromQuery] bool? DepreciacionRealizada
            )
        {
            Respuesta respuesta;
            try
            {
                //Respuesta
                respuesta = await _consultasService.Get(TipoActivo, Departamento,  FechaRegistroDesde,
             FechaRegistroHasta, AnioDepreaciacionDesde, AnioDepreciacionHasta,
             DepreciacionAcumuladaDesde, DepreciacionAcumuladaHasta, DepreciacionRealizada);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

    }
}
