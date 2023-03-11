using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IDepartamentoService<T>
    {
        Task<DepartamentoGetDTO> Get(int id);

        Task<List<DepartamentoGetDTO>> Get();

        Task<Departamento> Post(DepartamentoCreateDTO departamentoDTO);

        Task Put(DepartamentoUpdateDTO departamentoDTO, DepartamentoGetDTO departamento);

        Task Delete(int id);

        Task Delete(DepartamentoGetDTO departamento);
    }
}
