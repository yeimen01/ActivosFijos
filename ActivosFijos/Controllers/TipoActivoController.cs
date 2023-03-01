using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoActivoController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public TipoActivoController(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoActivo>>> Get()
        {
            return await DbContext.TipoActivo.Include(x => x.ActivosFijos).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TipoActivo>> Get(int id)
        {
            var tipoActivo = await DbContext.TipoActivo.
                Include(x => x.ActivosFijos).
                FirstOrDefaultAsync(x => x.Id == id);

            if (tipoActivo == null)
            {
                return NotFound("");
            }

            return tipoActivo;
        }

        [HttpPost]
        public async Task<ActionResult> Post(TipoActivoCreateDTO tipoActivoDTO)
        {
            //Mapping Information
            TipoActivo tipoActivo = mapper.Map<TipoActivo>(tipoActivoDTO);

            //Adding information
            DbContext.Add(tipoActivo);
            await DbContext.SaveChangesAsync();

            return Ok("Tipo de activo agregado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TipoActivoUpdateDTO tipoActivoDTO, int id)
        {
            //Verifying id
            if (tipoActivoDTO.Id != id)
            {
                return BadRequest("El id proporcionado no coincide con el id del tipo de activo.");
            }

            //Verifying existense
            var tipoActivo = await DbContext.TipoActivo.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoActivo == null)
            {
                return NotFound();
            }

            if (!Enum.IsDefined(typeof(Estado), tipoActivo.Estado))
            {
                return BadRequest("El estado suminstrado no existe.");
            }

            //Mapping information
            mapper.Map(tipoActivoDTO, tipoActivo);

            //Updating information
            DbContext.Entry(tipoActivo).State = EntityState.Modified;
            DbContext.Update(tipoActivo);
            await DbContext.SaveChangesAsync();

            return Ok("Tipo de activo actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await DbContext.TipoActivo.AnyAsync(x=> x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            DbContext.Remove(new TipoActivo { Id = id});
            await DbContext.SaveChangesAsync();
            return Ok("Tipo de activo borrado borrado correctamente.");
        }
    }
}
