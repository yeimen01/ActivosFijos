using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivosFijos.Model.Entities;
using ActivosFijos.Data.Interfaces;
using ActivosFijos.Model.Utilities;
using System.Net;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService<Empleado> _empleadoService;
        private readonly IDepartamentoService<Departamento> _departamentoService;

        public EmpleadoController(IEmpleadoService<Empleado> _empleadoService, IDepartamentoService<Departamento> _departamentoService)
        {
            this._empleadoService = _empleadoService;
            this._departamentoService = _departamentoService;
        }

        [HttpGet("{id:int}")]
        public async Task<ObjectResult> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Get By Id service
                respuesta = await _empleadoService.Get(id);

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
                //Get All
                respuesta = await _empleadoService.Get();
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmpleadoCreateDTO empleadoDTO)
        {
            Respuesta respuesta;
            try
            {
                //Post service
                respuesta = await _empleadoService.Post(empleadoDTO);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ObjectResult> Put(EmpleadoUpdateDTO empleadoDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Put service
                respuesta = await _empleadoService.Put(empleadoDTO, id);
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
                respuesta = await _empleadoService.Delete(id);
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
