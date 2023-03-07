using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await DbContext.Departamento.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Departamento>> Get(int id)
        {
            var departamento = await DbContext.Departamento.
                Include(x=> x.Empleados).
                FirstOrDefaultAsync(x => x.Id == id);

            if (departamento == null)
            {
                return NotFound();
            }


            return departamento;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DepartamentoCreateDTO departamentoDTO)
        {
            //Mapping information
            Departamento departamento = mapper.Map<Departamento>(departamentoDTO);

            //Adding the information
            DbContext.Add(departamento);
            await DbContext.SaveChangesAsync();

            return Ok(departamento);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(DepartamentoUpdateDTO departamentoDTO, int id)
        {
            //Verifying id
            if (departamentoDTO.Id != id)
            {
                return BadRequest("El id proporcionado no coincide con el id del departamento");
            }

            //Verifying existense
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x => x.Id == id);

            if (departamento == null)
            {
                return NotFound();
            }

            if (!Enum.IsDefined(typeof(Estado), departamento.Estado))
            {
                return BadRequest("El estado suminstrado no existe.");
            }

            //Mapping information
            mapper.Map(departamentoDTO, departamento);

            //Updating information
            DbContext.Entry(departamento).State = EntityState.Modified;
            DbContext.Update(departamento);
            await DbContext.SaveChangesAsync();

            return Ok("Departamento actualizado correctamente");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await DbContext.Departamento.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            //Deleting information
            DbContext.Remove(new Departamento() { Id = id});
            await DbContext.SaveChangesAsync();
            return Ok("Departamento borrado correctamente");
        }
    }
}
