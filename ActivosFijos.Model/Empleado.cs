using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private string NombreCompleto {
            get
            {
                return $"{Nombre} {Apellido}";
            }
        }

        public string Cedula { get; set; }
        public int DepartamentoId { get; set; }

        public Departamento Departamento { get; set; }
        public TipoPersona TipoPersona { get; set; }
        public DateTime? FechaIngreso { get; set; }
    }
}
