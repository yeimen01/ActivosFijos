using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ActivosFijos.Model.Entities
{
    public class TipoActivo
    {
        public TipoActivo()
        {
            Estado = 0;
        }

        public int Id { get; set; }

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

        public Estado Estado { get; set; }

        public List<ActivoFijo> ActivosFijos { get; set; }
    }
}
