using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface ICalculoDepreciacionService<T> : IRepository<CalculoDepreciacion, CalculoDepreciacionCreateDTO, CalculoDepreciacionUpdateDTO>
    {
        Task<Respuesta> GetByActivoFijo(int activoFijoId);
    }
}
