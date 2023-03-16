using ActivosFijos.Model.DTO;
using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace ActivosFijos.Model.Entities
{
    public class Empleado
    {
        public Empleado()
        {
            Estado = 0;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        private string NombreCompleto
        {
            get
            {
                return $"{Nombre} {Apellido}";
            }
        }
        public string Cedula { get; set; }

        public int DepartamentoId { get; set; }

        [JsonIgnore]
        public Departamento Departamento { get; set; }

        [NotMapped] public string DepartamentoDescripcion { get; set; }

        public TipoPersona TipoPersona { get; set; }

        public DateTime? FechaIngreso { get; set; }

        public Estado Estado { get; set; }
    }
}
