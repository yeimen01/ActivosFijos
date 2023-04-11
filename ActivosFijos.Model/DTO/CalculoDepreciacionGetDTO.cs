using ActivosFijos.Model;
using ActivosFijos.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class CalculoDepreciacionGetDTO
    {
        public int Id { get; set; }
        public int AñoProceso { get; set; }
        public int MesProceso { get; set; }
        public int ActivoFijoId { get; set; }
        public string DescripcionActivosFijos { get; set; }
        public DateTime? FechaProceso { get; set; }
        public double MontoDepreciado { get; set; }
        public double DepreciacionAcumulada { get; set; }
        public string CuentaCompra { get; set; }
        public string CuentaDepreciacion { get; set; }

        public int? AsientoContableId { get; set; }
        //public List<ActivoFijo> ActivoFijos { get; set; } no es necesario
    }
}
