using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Azure.Core;
using Azure;

namespace ActivosFijos.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public DepartamentoController(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext; 
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>> Get()
        {
            Respuesta response = new Respuesta();
            try
            {
                var departamentos = await DbContext.Departamento.
                    Include(x => x.Empleados).
                    ToListAsync();

                if (departamentos == null)
                {
                    return NotFound();
                }

                //Respuesta
                response = MG.Respuesta(HttpStatusCode.OK, "Exito", departamentos);
            }
            catch (Exception ex)
            {
                response = MG.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Departamento>> Get(int id)
        {
            Respuesta response = new Respuesta();

            try
            {
                var departamento = await DbContext.Departamento.
                Include(x => x.Empleados).
                FirstOrDefaultAsync(x => x.Id == id);

                if (departamento == null)
                {
                    return NotFound();
                }

                //Respuesta
                response = MG.Respuesta(HttpStatusCode.OK, "Exito", departamento);
            }
            catch (Exception ex)
            {
                response = MG.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DepartamentoCreateDTO departamentoDTO)
        {
            Respuesta response = new Respuesta();

            try
            {
                //Mapping information
                Departamento departamento = mapper.Map<Departamento>(departamentoDTO);

                //Adding the information
                DbContext.Add(departamento);
                await DbContext.SaveChangesAsync();

                //Respuesta
                response = MG.Respuesta(HttpStatusCode.OK, "Exito", departamento);
            }
            catch (Exception ex)
            {
                response = MG.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(DepartamentoUpdateDTO departamentoDTO, int id)
        {
            Respuesta response = new Respuesta();

            try
            {
                //Verifying id
                if (departamentoDTO.Id != id)
                {
                    //Respuesta
                    response = MG.Respuesta(HttpStatusCode.BadRequest, "El id proporcionado no coincide con el id del departamento");
                    return BadRequest(response);

                }

                //Verifying existense
                var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x => x.Id == id);

                if (departamento == null)
                {
                    //Respuesta
                    response = MG.Respuesta(HttpStatusCode.BadRequest, "No se encontró el departamento");
                    return NotFound(response);
                }

                if (!Enum.IsDefined(typeof(Estado), departamento.Estado))
                {
                    //Respuesta
                    response = MG.Respuesta(HttpStatusCode.NotFound, "El estado suminstrado no es valido.");
                    return NotFound(response);
                }

                //Mapping information
                mapper.Map(departamentoDTO, departamento);

                //Updating information
                DbContext.Entry(departamento).State = EntityState.Modified;
                DbContext.Update(departamento);
                await DbContext.SaveChangesAsync();

                //Respuesta
                response = MG.Respuesta(HttpStatusCode.OK, "Departamento actualizado correctamente", departamento);
            }
            catch (Exception ex)
            {
                response = MG.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok(response);
           }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Respuesta response = new Respuesta();

            try
            {
                var existe = await DbContext.Departamento.AnyAsync(x => x.Id == id);

                if (!existe)
                {
                    //Respuesta
                    response = MG.Respuesta(HttpStatusCode.NotFound, "No se encontró el departamento que quiere borrar");
                    return NotFound(response);
                }

                //Deleting information
                DbContext.Remove(new Departamento() { Id = id });
                await DbContext.SaveChangesAsync();

                //Respuesta
                response = MG.Respuesta(HttpStatusCode.OK, "Departamento borrado correctamente");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response = MG.Respuesta(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Ok(response);
        }
    }
}
