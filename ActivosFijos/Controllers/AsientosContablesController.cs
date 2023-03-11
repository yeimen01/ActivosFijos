using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActivosFijos.Model.Entities;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsientosContablesController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public AsientosContablesController(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AsientosContables>>> Get()
        {
            return await DbContext.AsientosContables.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AsientosContables>> Get(int id)
        {
            var asientoContable = await DbContext.AsientosContables.FirstOrDefaultAsync(x => x.Id == id);

            if (asientoContable == null)
            {
                return NotFound();
            }

            return asientoContable;
        }

        [HttpPost]
        public async Task<ActionResult> Post(AsientosContablesCreateDTO asientosContablesCreateDTO)
        {
            //Mapping information
            AsientosContables asientoContable= mapper.Map<AsientosContables>(asientosContablesCreateDTO);

            if (!Enum.IsDefined(typeof(TipoMovimiento), asientosContablesCreateDTO.TipoMovimiento))
            {
                return BadRequest("El tipo de movimiento suministrado no existe.");
            }

            //Adding the information
            DbContext.Add(asientoContable);
            await DbContext.SaveChangesAsync();

            return Ok("Asiento contable registrado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(AsientosContablesUpdateDTO asientosContablesUpdateDTO, int id)
        {
            //Verifying id
            if (asientosContablesUpdateDTO.Id != id)
            {
                return BadRequest("El id proporcionado no coincide con el id del asiento contable.");
            }

            //Verifying existense
            var asientoContable = await DbContext.AsientosContables.AnyAsync(x => x.Id == id);

            if (!asientoContable)
            {
                return NotFound("No existe el id del asiento contable que desea actualizar.");
            }

            if (Enum.IsDefined(typeof(TipoMovimiento), asientosContablesUpdateDTO.TipoMovimiento))
            {
                return BadRequest("El tipo de movimiento suministrado no existe.");
            }

            //Mapping information
            mapper.Map(asientosContablesUpdateDTO, asientoContable);

            //Updating information
            DbContext.Entry(asientoContable).State = EntityState.Modified;
            DbContext.Update(asientoContable);
            await DbContext.SaveChangesAsync();

            return Ok("Asiento contable actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeAsientoContable = await DbContext.AsientosContables.AnyAsync(x => x.Id == id);

            if (!existeAsientoContable)
            {
                return NotFound("No se encontró el asiento contable.");
            }

            //Deleting information
            DbContext.Remove(new AsientosContables { Id = id });
            await DbContext.SaveChangesAsync();
            return Ok("Asiento contable borrado correctamente.");
        }


    }
}
