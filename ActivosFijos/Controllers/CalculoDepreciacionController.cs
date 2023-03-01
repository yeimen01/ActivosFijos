using ActivosFijos.Data;
using ActivosFijos.Model.DTO;
using ActivosFijos.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ActivosFijos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculoDepreciacionController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper Mapper;

        public CalculoDepreciacionController(ApplicationDbContext DbContext, IMapper Mapper)
        {
            this.DbContext = DbContext;
            this.Mapper = Mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CalculoDepreciacion>>> Get()
        {
            return await DbContext.CalculoDepreciacion.
                Include(x=> x.ActivosFijos).
                ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CalculoDepreciacion>> Get(int id)
        {
            var calculoDepreciacion = await DbContext.CalculoDepreciacion.
                Include(x=> x.ActivosFijos).
                FirstOrDefaultAsync(x=> x.Id == id);

            if (calculoDepreciacion == null)
            {
                return NotFound();
            }

            return calculoDepreciacion;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CalculoDepreciacionCreateDTO calculoDepreciacionCreateDTO)
        {
            //Mapping information
            CalculoDepreciacion calculoDepreciacion = Mapper.Map<CalculoDepreciacion>(calculoDepreciacionCreateDTO);

            var activoFijo = await DbContext.TipoActivo.
                FindAsync(calculoDepreciacionCreateDTO.ActivoFijoId);

            if (activoFijo == null)
            {
                return NotFound("El activo fijo no existe.");
            }

            DbContext.Add(calculoDepreciacion);
            await DbContext.SaveChangesAsync();
            return Ok("El calculo de depreciacion se ha creado correctamente");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CalculoDepreciacionUpdateDTO calculoDepreciacionUpdateDTO, int id)
        {
            if (calculoDepreciacionUpdateDTO.Id != id)
            {
                return BadRequest("El id proporcionado no coincide con el id del calculo de depreciacion");
            }

            //Verifying existense
            var calculoDepreciacion = await DbContext.CalculoDepreciacion.FindAsync(id);

            if (calculoDepreciacion == null)
            {
                return NotFound("El calculo de depreciacion a actualizar no existe.");
            }

            //Verifying existense
            var activoFijo = await DbContext.TipoActivo.
                FindAsync(calculoDepreciacionUpdateDTO.ActivoFijoId);

            if (activoFijo == null)
            {
                return NotFound("El activo fijo no existe.");
            }

            //Mappping information
            Mapper.Map(calculoDepreciacionUpdateDTO, calculoDepreciacion);

            DbContext.Entry(calculoDepreciacion).State = EntityState.Modified;
            DbContext.Update(calculoDepreciacion);
            await DbContext.SaveChangesAsync();

            return Ok("El calculo de depreciacion se ha actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            //Verifying existense
            var calculoDepreciacion = await DbContext.CalculoDepreciacion.FindAsync(id);

            if (calculoDepreciacion == null)
            {
                return NotFound("El calculo de depreciacion a actualizar no existe.");
            }

            DbContext.Remove(new CalculoDepreciacion { Id = id });
            await DbContext.SaveChangesAsync();

            return Ok("El calculo de depreciacion se ha borrado correctamente.");
        }
    }
}
