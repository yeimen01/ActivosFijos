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
        public async Task<ActionResult<DepartamentoGetDTO>> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                //GetById service
                var departamento = await _departamentoService.Get(id);

                if (departamento == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.IdNotFound);
                    return NotFound(respuesta);
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

        [HttpGet]
        public async Task<ActionResult<List<DepartamentoGetDTO>>> Get()
        {
            Respuesta respuesta;
            try
            {
                //Get all service
                var departamentos = await _departamentoService.Get();

                if (departamentos == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, Utilities.NotFound);
                    return NotFound(respuesta);
                }

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Exito", departamentos);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500, respuesta);
            }

            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DepartamentoCreateDTO departamentoDTO)
        {
            Respuesta respuesta;
            try
            {
                //Post service
                var departamento = await _departamentoService.Post(departamentoDTO);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.Created, "Exito", departamentoDTO);
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
        public async Task<ActionResult> Put(DepartamentoUpdateDTO departamentoDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Verifying id
                if (departamentoDTO.Id != id)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "El id proporcionado no coincide con el id del departamento.");
                    return BadRequest(respuesta);
                }

                //Verifying existense
                var departamento = await _departamentoService.Get(id);

                if (departamento == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encontró el departamento.");

                    return NotFound(respuesta);
                }

                //Verifying if enum is correct
                if (!Enum.IsDefined(typeof(Estado), departamento.Estado))
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "El estado suminstrado no existe.");
                    return BadRequest(respuesta);
                }

                //Put service
                await _departamentoService.Put(departamentoDTO, departamento);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Departamento actualizado correctamente", departamento);
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
                var departamento = await _departamentoService.Get(id);

                if (departamento == null)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.NotFound, "No se encontró el departamento.");
                    return NotFound(respuesta);
                }

                if (departamento.Empleados.Count > 0)
                {
                    //Respuesta
                    respuesta = Utilities.Respuesta(HttpStatusCode.BadRequest, "No se puede borrar el departamento, porque ya tiene empleados.");
                    return BadRequest(respuesta);
                }

                //Delete service
                await _departamentoService.Delete(departamento);

                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.OK, "Departamento borrado correctamente");

            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode(500,respuesta);
            }

            return Ok(respuesta);
        }
    }
}
