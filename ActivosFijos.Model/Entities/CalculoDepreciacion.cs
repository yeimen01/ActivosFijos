﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ActivosFijos.Model.Entities
{
    public class CalculoDepreciacion
    {
        public CalculoDepreciacion()
        {
            AsientoContableId = null;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(2000, 2099, ErrorMessage = Utilities.Utilities.MsgRange)]
        [Display(Name = "año de proceso")]
        public int AñoProceso { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, 12, ErrorMessage = Utilities.Utilities.MsgRange)]
        [Display(Name = "mes de proceso")]
        public int MesProceso { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "activo fijo")]
        public int ActivoFijoId { get; set; }

        [JsonIgnore]
        public ActivoFijo ActivosFijos { get; set; }

        public int? AsientoContableId { get; set; } = null;

        //[JsonIgnore]
        //public AsientosContables AsientoContable {get; set;}
        public DateTime? FechaProceso { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "monto depreciado")]
        public double MontoDepreciado { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Range(1, Int32.MaxValue, ErrorMessage = Utilities.Utilities.MsgBiggerThanZero)]
        [Display(Name = "depreciación acumulada")]
        public double DepreciacionAcumulada { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "cuenta de compra")]
        public string CuentaCompra { get; set; }

        [Required(ErrorMessage = Utilities.Utilities.MsgRequired)]
        [Display(Name = "cuenta de depreciación")]
        public string CuentaDepreciacion { get; set; }
    }
}
