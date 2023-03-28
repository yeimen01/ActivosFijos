using ActivosFijos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ActivosFijos.Model.DTO
{
    public class ActivoFijoCreateDTO
    {
        [Display(Name = "descripción")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(30, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "departamento")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "tipo de activo")]
        public int TipoActivoId { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "valor de compra")]
        public double ValorCompra { get; set; }


        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "valor de depreciación")]
        public double ValorDepreciacion { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(0, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "depreciación acumulada")]
        public double DepreciacionAcumulada { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(2000, 2099, ErrorMessage = Utilities.Utilities.MsgRange)]
        [Display(Name = "año de depreciación")]
        public int AnioDepreciacion { get; set; }
    }
}
