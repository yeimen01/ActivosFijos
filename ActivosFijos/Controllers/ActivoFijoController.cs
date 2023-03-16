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

        public ActivoFijoController(IActivoFijoService<ActivoFijo> _activoFijoService) 
        {
            this._activoFijoService = _activoFijoService;
        }

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Get service
                respuesta = await _activoFijoService.Get(id);
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
                //Get all serice
                respuesta = await _activoFijoService.Get();
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }
        [HttpPost]
        public async Task<ObjectResult> Post(ActivoFijoCreateDTO activoFijoCreateDTO)
        {
            Respuesta respuesta;
            try
            {
                //Post service
                respuesta = await _activoFijoService.Post(activoFijoCreateDTO);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }
        [HttpPut("{id:int}")]
        public async Task<ObjectResult> Put(ActivoFijoUpdateDTO activoFijoUpdateDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Put service
                respuesta = await _activoFijoService.Put(activoFijoUpdateDTO, id);
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
                respuesta = await _activoFijoService.Delete(id);
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
