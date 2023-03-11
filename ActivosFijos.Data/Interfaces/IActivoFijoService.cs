using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IActivoFijoService<T>
    {
        Task<ActivoFijoGetDTO> Get(int id);

        Task<List<ActivoFijoGetDTO>> Get();

        Task<ActivoFijo> Post(ActivoFijoCreateDTO activoFijo);

        Task Put(ActivoFijoUpdateDTO activoFijoUpdateDTO, ActivoFijoGetDTO activoFijoGetDTO);

        Task Delete(int id);

        Task Delete(ActivoFijoGetDTO activofijo);
    }
}
