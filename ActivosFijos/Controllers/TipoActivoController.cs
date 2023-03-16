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

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Get by id service
                respuesta = await _tipoActivoService.Get(id);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpGet]
        public async Task<ObjectResult> Get()
        {
            Respuesta respuesta;
            try
            {
                //Get all
                respuesta = await _tipoActivoService.Get();
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPost]
        public async Task<ObjectResult> Post(TipoActivoCreateDTO tipoActivoDTO)
        {
            Respuesta respuesta;
            try
            {
                //Post service
                respuesta = await _tipoActivoService.Post(tipoActivoDTO);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ObjectResult> Put(TipoActivoUpdateDTO tipoActivoDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Put service
                respuesta = await _tipoActivoService.Put(tipoActivoDTO, id);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpDelete("{id:int}")]
        public async Task<ObjectResult> Delete(int id)
        {
            Respuesta respuesta;
            try
            {
                //Delete service
                respuesta = await _tipoActivoService.Delete(id);
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
