using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IActivoFijoService<T> : IRepository<ActivoFijo, ActivoFijoCreateDTO, ActivoFijoUpdateDTO>
    {
        //Task<ActivoFijoGetDTO> Get(int id);

        //Task<List<ActivoFijoGetDTO>> Get();

        //Task<ActivoFijo> Post(ActivoFijoCreateDTO activoFijo);

        //Task Put(ActivoFijoUpdateDTO activoFijoUpdateDTO, int id);

        //Task Delete(int id);
    }
}
