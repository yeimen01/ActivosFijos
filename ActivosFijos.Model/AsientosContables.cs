using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model
{
    public class AsientosContables
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Inventario { get; set; }
        public string CuentaContable { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public DateTime? FechaAsiento { get; set; }
        public double MontoAsiento { get; set; }
        public bool Estado { get; set; }
    }
}
