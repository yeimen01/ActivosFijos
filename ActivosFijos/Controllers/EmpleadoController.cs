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
    public class EmpleadoController : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IMapper mapper;

        public EmpleadoController(ApplicationDbContext DbContext, IMapper mapper)
        {
            this.DbContext = DbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> Get()
        {
            var empleados = await DbContext.Empleado
                .Include(x => x.Departamento)
                .Select(value => new Empleado()
                {
                    Id = value.Id,
                    Nombre = value.Nombre,
                    Apellido = value.Apellido,
                    Cedula = value.Cedula,
                    DepartamentoId = value.DepartamentoId,
                    Estado = value.Estado,
                    FechaIngreso = value.FechaIngreso,
                    DepartamentoDescripcion = value.Departamento.Descripcion
                })
                .ToListAsync();
            
            return Ok(empleados);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empleado>> Get(int id)
        {
            var empleado = await DbContext.Empleado.
                Include(x => x.Departamento).
                FirstOrDefaultAsync(x => x.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmpleadoCreateDTO empleadoDTO)
        {
            //Mapping information
            Empleado empleado = mapper.Map<Empleado>(empleadoDTO);

            //Verifying the exitanse of the department
            var departamento = await DbContext.Departamento.FirstOrDefaultAsync(x => x.Id == empleado.DepartamentoId);

            if (departamento == null)
            {
                return BadRequest("No existe el departamento del empleado que desea agregar.");
            }

            if (!Enum.IsDefined(typeof(TipoPersona), empleadoDTO.TipoPersona))
            {
                return BadRequest("El tipo de persona suministrado no existe.");
            }

            //Adding the information
            DbContext.Add(empleado);
            await DbContext.SaveChangesAsync();

            return Ok("Empleado agregado correctamente.");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EmpleadoUpdateDTO empleadoDTO, int id)
        {
            //Verifying id
            if (empleadoDTO.Id != id)
            {
                return BadRequest("El id proporcionado no coincide con el id del empleado.");
            }

            //Verifying existense
            var empleado = await DbContext.Empleado.FirstOrDefaultAsync(x => x.Id == id);

            if (empleado == null)
            {
                return NotFound("No existe el id del empleado que desea actualizar.");
            }

            if (!Enum.IsDefined(typeof(TipoPersona), empleadoDTO.TipoPersona))
            {
                return BadRequest("El tipo de persona suministrado no existe.");
            }

            /*if (!Enum.IsDefined(typeof(Estado), empleado.Estado))
            {
                return BadRequest("El estado suminstrado no existe.");
            }*/

            //Mapping information
            mapper.Map(empleadoDTO, empleado);

            //Updating information
            DbContext.Entry(empleado).State = EntityState.Modified;
            DbContext.Update(empleado);
            await DbContext.SaveChangesAsync();

            return Ok("Empleado actualizado correctamente.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existeEmpleado = await DbContext.Empleado.AnyAsync(x => x.Id == id);

            if (!existeEmpleado)
            {
                return NotFound("No se encontró el empleado.");
            }

            //Deleting information
            DbContext.Remove(new Empleado { Id = id});
            await DbContext.SaveChangesAsync();
            return Ok("Empleado borrado correctamente.");
        }

    }
}
