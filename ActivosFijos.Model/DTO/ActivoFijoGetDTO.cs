using ActivosFijos.Model;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class ActivoFijoGetDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int DepartamentoId { get; set; }
        public string DescripcionDepartamento { get; set; }
        public int TipoActivoId { get; set; }
        public string DescripcionTipoActivo { get; set; }
        public string CuentaContableCompra { get; set; }
        public string CuentaContableDepreciacion { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public double ValorCompra { get; set; }
        public double ValorDepreciacion { get; set; }
        public double DepreciacionAcumulada { get; set; }
        public int AnioDepreciacion{ get; set; }
        public List<CalculoDepreciacion> CalculoDepreciaciones { get; set; }

    }
}
