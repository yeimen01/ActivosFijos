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
        public async Task<ActionResult<EmpleadoGetDTO>> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //Data
                var empleado = await _empleadoService.Get(id);

                if (empleado == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
                    return NotFound(respuesta);
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", empleado);

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500,respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoGetDTO>>> Get()
        {
            Respuesta respuesta;
            try
            {
                //Data
                var empleados = await _empleadoService.Get();

                if (empleados == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
                    return NotFound(respuesta);
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", empleados);

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
        public async Task<ActionResult> Post(EmpleadoCreateDTO empleadoDTO)
        {
            Respuesta respuesta;
            try
            {
                //Verifying the exitanse of the department
                var departamento = await _departamentoService.Get(empleadoDTO.DepartamentoId);

                if (departamento == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el departamento del empleado que desea agregar.");
                    return NotFound(respuesta);
                }

                //Verifying tipo persona
                if (!Enum.IsDefined(typeof(TipoPersona), empleadoDTO.TipoPersona))
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El tipo de persona suministrado no existe.");
                    return BadRequest(respuesta);
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", departamento);

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
        public async Task<ActionResult> Put(EmpleadoUpdateDTO empleadoDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Verifying id
                if (empleadoDTO.Id != id)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del empleado.");
                    return NotFound(respuesta);
                }

                //Verifying existense
                var empleado = await _empleadoService.Get(id);

                if (empleado == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del empleado que desea actualizar.");
                    return NotFound(respuesta);
                }

                if (!Enum.IsDefined(typeof(TipoPersona), empleadoDTO.TipoPersona))
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El tipo de persona suministrado no existe.");
                    return BadRequest(respuesta);
                }

                if (!Enum.IsDefined(typeof(Estado), empleado.Estado))
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El estado suminstrado no existe.");
                    return BadRequest(respuesta);
                }

                //Put service
                await _empleadoService.Put(empleadoDTO, empleado);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Empleado actualizado correctamente.", empleado);

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
                var existeEmpleado = await _empleadoService.Get(id);

                if (existeEmpleado == null)
                {
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No existe el id del empleado que desea borrar.");
                    return NotFound(respuesta);
                }

                //Delete service
                await _empleadoService.Delete(existeEmpleado);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Empleado borrado correctamente.");

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, respuesta);
            }

            return Ok(respuesta);
        }

    }
}
