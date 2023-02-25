using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model
{
    public class TipoActivo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContableCompra { get; set; }
        public string CuentaContableDepreciacion { get; set; }
        public bool Estado { get; set; }
        public List<ActivosFijos> ActivosFijos { get; set; }
    }
}
