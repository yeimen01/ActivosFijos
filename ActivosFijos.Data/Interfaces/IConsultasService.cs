using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Data.Interfaces
{
    public interface IConsultasService<T>
    {
        Task<Respuesta> Get(int? idTipoActivo, int? idDepartamento, DateTime? fechaRegistroDesde,
            DateTime? fechaRegistroHasta, int? anioDepreaciacionDesde, int? anioDepreciacionHasta, double? depreciacionDesde,
            double? depreciacionHasta, bool? depreciacionRealizada);
    }
}
