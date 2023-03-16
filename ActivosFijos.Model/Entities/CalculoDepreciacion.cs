using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ActivosFijos.Model.Entities
{
    public class CalculoDepreciacion
    {

        public int Id { get; set; }
        public int AñoProceso { get; set; }
        public int MesProceso { get; set; }
        public int ActivoFijoId { get; set; }
        [JsonIgnore]
        public ActivoFijo ActivosFijos { get; set; }
        //public int AsientoContableId { get; set; }
        //[JsonIgnore]
        //public AsientosContables AsientoContable {get; set;}
        public DateTime? FechaProceso { get; set; }
        public double MontoDepreciado { get; set; }
        public double DepreciacionAcumulada { get; set; }
        public string CuentaCompra { get; set; }
        public string CuentaDepreciacion { get; set; }

    }
}
