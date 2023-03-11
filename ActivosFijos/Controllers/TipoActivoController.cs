using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivosFijos.Model.Entities;
using ActivosFijos.Data.Interfaces;
using ActivosFijos.Data.Interfaces.Services;
using ActivosFijos.Model.Utilities;
using System.Net;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoActivoController : ControllerBase
    {
        private readonly ITipoActivoService<TipoActivo> _tipoActivoService;

        public TipoActivoController(ITipoActivoService<TipoActivo> _tipoActivoService)
        {
            this._tipoActivoService = _tipoActivoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoActivo>>> Get()
        {
            Respuesta respuesta;
            try
            {
                //Data
                var tiposActivos = await _tipoActivoService.Get();
               

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", tiposActivos);

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500,respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoActivo>> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Data
                var tipoActivo = await _tipoActivoService.Get(id);

                if (tipoActivo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
                    return NotFound(respuesta);
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", tipoActivo);

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500,respuesta);
            }

            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Post(TipoActivoCreateDTO tipoActivoDTO)
        {
            Respuesta respuesta;
            try
            {
                //Data
                var tipoActivo = await _tipoActivoService.Post(tipoActivoDTO);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Tipo de activo agregado correctamente.", tipoActivo);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, respuesta);
            }

            return Ok(respuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TipoActivoUpdateDTO tipoActivoDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Verifying id
                if (tipoActivoDTO.Id != id)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del tipo de activo.");
                    return NotFound(respuesta);
                }

                //Verifying existense
                var tipoActivo = await _tipoActivoService.Get(id);

                if (tipoActivo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
                    return NotFound(respuesta);
                }

                if (!Enum.IsDefined(typeof(Estado), tipoActivo.Estado))
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El estado suminstrado no existe.");
                    return BadRequest(respuesta);
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Tipo de activo actualizado correctamente.", tipoActivo);

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, respuesta);
            }


            return Ok(respuesta);
        }    

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Respuesta respuesta;
            try
            {
                var existeTipoActivo = await _tipoActivoService.Get(id);

                if (existeTipoActivo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
                    return NotFound(respuesta);
                }

                if (existeTipoActivo.ActivosFijos.Count > 0)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El tipo de activo no se puede borrar, ya que posee activos fijos.");
                    return BadRequest(respuesta);
                }

                //Deleting service
                await _tipoActivoService.Delete(existeTipoActivo);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Tipo de activo borrado correctamente");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500,respuesta);
            }


        }
    }
}
