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
    public class DepartamentoUpdateDTO
    {
        public int Id { get; set; }

        [Display(Name = "descripción")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(30, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Descripcion { get; set; }

        //public Estado Estado { get; set; }
    }
}
