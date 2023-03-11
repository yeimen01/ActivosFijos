using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ActivosFijos.Model.Entities;
using Microsoft.AspNetCore.DataProtection.Internal;
using ActivosFijos.Data.Interfaces;
using ActivosFijos.Model.Utilities;
using System.Net;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivoFijoController : ControllerBase
    {
        private readonly IActivoFijoService<ActivoFijo> _activoFijoService;
        private readonly ITipoActivoService<TipoActivo> _tipoActivoService;
        private readonly IDepartamentoService<Departamento> _departamentoService;


        public ActivoFijoController(
            IActivoFijoService<ActivoFijo> _activoFijoService, 
            ITipoActivoService<TipoActivo> _tipoActivoService,
            IDepartamentoService<Departamento> _departamentoService)
        {
            this._activoFijoService = _activoFijoService;
            this._tipoActivoService = _tipoActivoService;
            this._departamentoService = _departamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActivoFijo>>> Get()
        {
            Respuesta respuesta;
            try
            {
                //Data
                var activoFijo = await _activoFijoService.Get();

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", activoFijo);

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(statusCode: 500, respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActivoFijo>> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Data
                var activoFijo = await _activoFijoService.Get(id);

                if (activoFijo == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
                    return NotFound();
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", activoFijo);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(statusCode: 500,respuesta);
            }

            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActivoFijoCreateDTO activoFijoCreateDTO)
        {
            Respuesta respuesta;
            try
            {
                var departamento = _departamentoService.Get(activoFijoCreateDTO.DepartamentoId);

                if (departamento == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotro el departamento.");
                    return NotFound(respuesta);
                }

                var tipoActivo = _tipoActivoService.Get(activoFijoCreateDTO.TipoActivoId);

                if (tipoActivo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotro el tipo de activo.");
                    return NotFound(respuesta);
                }

                //Post service
                var activoFijo = await _activoFijoService.Post(activoFijoCreateDTO);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", activoFijo);

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(statusCode: 500, respuesta);
            }

            return Ok(respuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActivoFijoUpdateDTO activoFijoUpdateDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                if (activoFijoUpdateDTO.Id != id)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El id proporcionado no coincide con el id del activo fijo.");
                    return BadRequest(respuesta);
                }

                //Activo fijo
                var activoFijo = await _activoFijoService.Get(id);

                if (activoFijo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
                    return NotFound(respuesta);
                }

                //Departamento
                var departamento = _departamentoService.Get(activoFijoUpdateDTO.DepartamentoId);

                if (departamento == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotro el departamento.");
                    return NotFound(respuesta);
                }

                //Tipo activo
                var tipoActivo = _tipoActivoService.Get(activoFijoUpdateDTO.TipoActivoId);

                if (tipoActivo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encotro el tipo de activo.");
                    return NotFound(respuesta);
                }

                //Update service
                await _activoFijoService.Put(activoFijoUpdateDTO, activoFijo);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", activoFijo);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(statusCode: 500, respuesta);
            }

            return Ok(respuesta);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Respuesta respuesta;
            try
            {
                var existeActivoFijo = await _activoFijoService.Get(id);

                if (existeActivoFijo == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
                    return NotFound();
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Activo fijo borrado correctamente.");

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(statusCode: 500, respuesta);
            }

            return Ok(respuesta);
        }
    }
}
