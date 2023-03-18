using ActivosFijos.Model;
using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class EmpleadoGetDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Cedula { get; set; }

        public int DepartamentoId { get; set; }

        public string DepartamentoDescripcion { get; set; }

        public string TipoPersona { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public string Estado { get; set; }
    }
}
