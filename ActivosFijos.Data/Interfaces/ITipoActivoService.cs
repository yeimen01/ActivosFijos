using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface ITipoActivoService<T>
    {
        Task<TipoActivoGetDTO> Get(int id);

        Task<List<TipoActivoGetDTO>> Get();

        Task<TipoActivo> Post(TipoActivoCreateDTO tipoActivoCreateDTO);

        Task Put(TipoActivoUpdateDTO tipoActivoUpdateDTO, TipoActivoGetDTO tipoActivoGetDTO);

        Task Delete(int id);

        Task Delete(TipoActivoGetDTO tipoActivoGetDTO);

    }
}
