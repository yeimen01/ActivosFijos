using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivoFijoController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper Mapper;

        public ActivoFijoController(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActivoFijo>>> Get()
        {
            return await DbContext.ActivosFijo.
                Include(x=> x.Departamento).
                Include(x=> x.TipoActivo).
                ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActivoFijo>> Get(int id)
        {
            var activoFijo = await DbContext.ActivosFijo.FirstOrDefaultAsync(x => x.Id == id);

            if (activoFijo == null)
            {
                return NotFound();
            }

            return activoFijo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActivoFijoCreateDTO activoFijoCreateDTO)
        {
            //Mapping information
            ActivoFijo activoFijo = Mapper.Map<ActivoFijo>(activoFijoCreateDTO);

            var tipoActivo = await DbContext.TipoActivo.FindAsync(activoFijoCreateDTO.TipoActivoId);

            if (tipoActivo == null)
            {
                return NotFound("El tipo de activo no existe.");
            }

            //Adding the information
            DbContext.Add(activoFijo);
            await DbContext.SaveChangesAsync();
            return Ok("Activo fijo agregado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActivoFijoUpdateDTO activoFijoUpdateDTO, int id)
        {
            if (activoFijoUpdateDTO.Id != id)
            {
                return BadRequest("El id proporcionado no coincide con el id del activo fijo");
            }

            //Verifying existense
            var activoFijo = await DbContext.ActivosFijo.
                FirstOrDefaultAsync(x => x.Id == id);

            if (activoFijo == null)
            {
                return NotFound("No existe el id del activo fijo que desea actualizar.");
            }

            //Verifying existense
            var existeDepartamento = await DbContext.Departamento.
                FirstOrDefaultAsync(x => x.Id == activoFijoUpdateDTO.DepartamentoId);

            if (existeDepartamento == null)
            {
                return NotFound("No existe el departamento del activo fijo que desea agregar.");
            }

            //Verifying existense
            var existeTipoActivo = await DbContext.TipoActivo.
                FirstOrDefaultAsync(x => x.Id == activoFijoUpdateDTO.TipoActivoId);

            if (existeTipoActivo == null)
            {
                return NotFound("No existe el tipo de activo del activo fijo que desea agregar.");
            }

            //Mapping information
            Mapper.Map(activoFijoUpdateDTO, activoFijo);

            //Updating information
            DbContext.Entry(activoFijo).State = EntityState.Modified;
            DbContext.ActivosFijo.Update(activoFijo);
            await DbContext.SaveChangesAsync();

            return Ok("Activo fijo actualizado correctamente");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeActivoFijo = await DbContext.ActivosFijo.
                FirstOrDefaultAsync(x=> x.Id == id);

            if (existeActivoFijo == null)
            {
                return NotFound();
            }

            DbContext.Remove(new ActivoFijo { Id = id });
            await DbContext.SaveChangesAsync();
            return Ok("Activo fijo borrado correctamente.");
        }
    }
}
