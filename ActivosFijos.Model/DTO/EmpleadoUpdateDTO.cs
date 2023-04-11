using ActivosFijos.Model;
using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ActivosFijos.Model.DTO
{
    public class EmpleadoUpdateDTO
    {
        public int Id { get; set; }

        [Display(Name = "nombre")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(20, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Nombre { get; set; }

        [Display(Name = "apellido")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(20, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Apellido { get; set; }

        [Display(Name = "cédula")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(11, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(11, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Cedula { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "departamento")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "tipo de persona")]
        public TipoPersona TipoPersona { get; set; }

        public DateTime? FechaIngreso { get; set; }
        
        //[Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        //[RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        //[Display(Name = "estado")]
        //public Estado Estado { get; set; }
    }
}
