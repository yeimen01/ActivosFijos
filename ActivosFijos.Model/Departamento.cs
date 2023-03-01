using ActivosFijos.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ActivosFijos.Model
{
    public class Departamento
    {
        public Departamento()
        {
            Estado = 0;
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Estado Estado { get; set; }
        public List<Empleado> Empleados { get; set; }
    }
}