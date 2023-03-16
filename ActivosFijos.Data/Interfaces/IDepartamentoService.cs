using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IDepartamentoService<T> : IRepository<Departamento, DepartamentoCreateDTO, DepartamentoUpdateDTO>
    {
        //Task<Respuesta> Get(int id);

        //Task<Respuesta> Get();

        //Task<Respuesta> Post(DepartamentoCreateDTO departamentoDTO);

        //Task<Respuesta> Put(DepartamentoUpdateDTO departamentoDTO, int id);

        //Task<Respuesta> Delete(int id);

    }
}
