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
    public class TipoActivoCreateDTO
    {
        public TipoActivoCreateDTO()
        {
            Estado = 0;
        }

        [Display(Name = "descripción")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(30, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Descripcion { get; set; }

        [Display(Name = "cuenta contable de compra")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        public string CuentaContableCompra { get; set; }

        [Display(Name = "cuenta contable de depreciacion")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        public string CuentaContableDepreciacion { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "estado")]
        public Estado Estado { get; set; }

    }
}
