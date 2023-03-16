using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IRepository<Entity, Create, Update> where Entity : class
    {
        Task<Respuesta> Get(int id);

        Task<Respuesta> Get();

        Task<Respuesta> Post(Create createDTO);

        Task<Respuesta> Put(Update UpdateDTO, int id);

        Task<Respuesta> Delete(int id);
    }
}
