using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivosFijos.Data.Interfaces.Services;
using ActivosFijos.Data.Interfaces;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Utilities;
using System.Net;
using System.Web.Http.Results;

namespace ActivosFijos.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService<Departamento> _departamentoService;

        public DepartamentoController(IDepartamentoService<Departamento> _departamentoService)
        {
            this._departamentoService = _departamentoService;

        }

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Get By Id service
                respuesta = await _departamentoService.Get(id);
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
                //Respuesta
                respuesta = await _departamentoService.Get();
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPost]
        public async Task<ObjectResult> Post(DepartamentoCreateDTO departamentoDTO)
        {
            Respuesta respuesta;
            try
            {
                //Post service
                respuesta = await _departamentoService.Post(departamentoDTO);                              
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ObjectResult> Put(DepartamentoUpdateDTO departamentoDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Put service
                respuesta = await _departamentoService.Put(departamentoDTO, id);
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
                respuesta = await _departamentoService.Delete(id);
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
