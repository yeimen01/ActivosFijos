using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface ICalculoDepreciacionService<T>
    {
        Task<CalculoDepreciacionGetDTO> Get(int id);

        Task<List<CalculoDepreciacionGetDTO>> Get();

        Task<CalculoDepreciacion> Post(CalculoDepreciacionCreateDTO calculoDepreciacionCreateDTO);

        Task Put(CalculoDepreciacionUpdateDTO calculoDepreciacionUpdateDTO, CalculoDepreciacionGetDTO calculoDepreciacionGetDTO);

        Task Delete(int id);

        Task Delete(CalculoDepreciacionGetDTO calculoDepreciacionGetDTO);
    }
}
