using ActivosFijos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class CalculoDepreciacionCreateDTO
    {
        public int AñoProceso { get; set; }
        public int MesProceso { get; set; }
        public int ActivoFijoId { get; set; }
        public DateTime? FechaProceso { get; set; }
        public double MontoDepreciado { get; set; }
        public double DepreciacionAcumulada { get; set; }
        public string CuentaCompra { get; set; }
        public string CuentaDepreciacion { get; set; }
    }
}
