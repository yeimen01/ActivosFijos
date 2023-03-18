using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ActivosFijos.Model.Entities
{
    public class ActivoFijo
    {
        public int Id { get; set; }


        [Display(Name = "descripción")]
        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [MaxLength(30, ErrorMessage = Utilities.Utilities.MsgMaxLetters)]
        [MinLength(3, ErrorMessage = Utilities.Utilities.MsgMinLetters)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "departamento")]
        public int DepartamentoId { get; set; }

        [JsonIgnore]
        public Departamento Departamento { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "tipo de activo")]
        public int TipoActivoId { get; set; }

        [JsonIgnore]
        public TipoActivo TipoActivo { get; set; }

        public DateTime? FechaRegistro { get; set; } = DateTime.Now;

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "valor de compra")]
        public double ValorCompra { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "valor de depreciación")]
        public double ValorDepreciacion { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "depreciación acumulada")]
        public double DepreciacionAcumulada { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(2000, 2099, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "año de depreciación")]
        public int AnioDepreciacion{ get; set; }

    }
}
