using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivosFijos.Model.Entities;
using ActivosFijos.Data.Interfaces;
using ActivosFijos.Model.Utilities;
using System.Net;
using System.Text;
using Newtonsoft.Json;

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

                var contabilizarDBModels = 
                        data.Select(value => new ContabilizarCreateDTO()
                    {
                        id_aux = 8,
                        nombre_aux = "Gasto depreciación Activos Fijos",
                        monto = "1000.00",
                        cuenta = 1,
                        id_EC = "AC500",
                        origen = "CR"
                    }).ToList();

                /*var contabilizarCRModels =  data.Select(value => new ContabilizarCreateDTO()
                {
                    id_aux = 8,
                    nombre_aux = $"Gasto depreciación Activos Fijos",
                    monto = value.MontoDepreciado.ToString(),
                    cuenta = 66,
                    origen = "CR",
                    id_EC = "AC7"
                }).ToList();

                contabilizarCRModels.AddRange(contabilizarDBModels);*/
                
                HttpClient client = new HttpClient();

                foreach (var item in contabilizarDBModels)
                {
                    
                    var result =await client.PostAsJsonAsync("https://contabilidadapi.azurewebsites.net/api_aux/SistCont/",item );
                    result.EnsureSuccessStatusCode();
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
