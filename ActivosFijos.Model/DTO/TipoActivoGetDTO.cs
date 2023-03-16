using ActivosFijos.Model;
using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class TipoActivoGetDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContableCompra { get; set; }
        public string CuentaContableDepreciacion { get; set; }
        public string Estado { get; set; }
        public List<ActivoFijo> ActivosFijos { get; set; }
    }
}
