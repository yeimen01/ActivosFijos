using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ActivosFijos.Model
{
    public class ActivoFijo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int DepartamentoId { get; set; }
        [JsonIgnore]
        public Departamento Departamento { get; set; }
        public int TipoActivoId { get; set; }
        [JsonIgnore]
        public TipoActivo TipoActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public double ValorCompra { get; set; }
        public double DepreciacionAcumulada { get; set; }

    }
}
