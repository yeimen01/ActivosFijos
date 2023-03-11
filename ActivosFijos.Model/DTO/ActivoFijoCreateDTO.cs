using ActivosFijos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class ActivoFijoCreateDTO
    {
        public string Descripcion { get; set; }
        public int DepartamentoId { get; set; }
        public int TipoActivoId { get; set; }
        public double ValorCompra { get; set; }
        public double ValorDepreciacion { get; set; }
        public int AnioDepreciacion{ get; set; }
    }
}
