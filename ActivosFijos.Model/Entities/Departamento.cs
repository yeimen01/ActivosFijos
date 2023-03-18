using ActivosFijos.Model.Enum;
using ActivosFijos.Model.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ActivosFijos.Model.Entities
{
    public class Departamento
    {
        public Departamento()
        {
            Estado = 0;
        }

        public int Id { get; set; }

        [Display(Name = "descripción")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(30, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Descripcion { get; set; }

        public Estado Estado { get; set; }

        public List<Empleado> Empleados { get; set; }
    }
}