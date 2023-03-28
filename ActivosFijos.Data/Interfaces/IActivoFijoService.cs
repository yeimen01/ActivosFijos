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
    }
}
