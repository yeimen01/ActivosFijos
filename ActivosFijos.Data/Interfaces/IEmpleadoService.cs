using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IEmpleadoService<T> : IRepository<Empleado, EmpleadoCreateDTO, EmpleadoUpdateDTO>
    {
        //Task<EmpleadoGetDTO> Get(int id);

        //Task<List<EmpleadoGetDTO>> Get();

        //Task<Empleado> Post(EmpleadoCreateDTO empleado);

        //Task Put(EmpleadoUpdateDTO empleadoUpdateDTO, int id);

        //Task Delete(int id);

    }
}
