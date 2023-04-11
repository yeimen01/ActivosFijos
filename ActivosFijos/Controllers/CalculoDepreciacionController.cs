using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
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
    public class CalculoDepreciacionController : ControllerBase
    {
        private readonly ICalculoDepreciacionService<CalculoDepreciacion> _calculoService;

        public CalculoDepreciacionController(ICalculoDepreciacionService<CalculoDepreciacion> _calculoService)
        {
            this._calculoService = _calculoService;
        }

        [HttpGet("Getbyactivo")]
        public async Task<ActionResult> GetByActivoFijo([FromQuery] int activoFijoId)
        {
            Respuesta respuesta;
            try
            {
                respuesta = await _calculoService.GetByActivoFijo(activoFijoId);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPost("contabilizar")]
        public async Task<IActionResult> Contabilizar([FromBody] Contabilizar contabilizar)
        {
            try
            {
                var data = await _calculoService.GetByIds(contabilizar.Ids);

                data.Select(value => new ContabilizarCreateDTO()
                    {
                        id_aux = 1,
                        nombre_aux = $"Depreciación ${value.ActivosFijos.Descripcion}",
                        monto = int.Parse(value.MontoDepreciado.ToString()),
                        cuenta = 1,
                        origen = "CR"
                    })
                    ;
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<CalculoDepreciacionGetDTO>> Get()
        {
            Respuesta respuesta;
            try
            {
                respuesta = await _calculoService.Get();
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CalculoDepreciacionGetDTO>> Get(int id)
        {
            Respuesta respuesta;
            try
            {
                respuesta = await _calculoService.Get(id);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CalculoDepreciacionCreateDTO calculoDepreciacionCreateDTO)
        {
            Respuesta respuesta;
            try
            {
                respuesta = await _calculoService.Post(calculoDepreciacionCreateDTO);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CalculoDepreciacionUpdateDTO calculoDepreciacionUpdateDTO, int id)
        {
            Respuesta respuesta;
            try
            {
                //Updating service
                respuesta = await _calculoService.Put(calculoDepreciacionUpdateDTO, id);
            }
            catch (Exception ex)
            {
                //Respuesta
                respuesta = Utilities.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Utilities.RespuestaActionResult(respuesta);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Respuesta respuesta;
            try
            {
                //Deleting service
                respuesta = await _calculoService.Delete(id);
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
