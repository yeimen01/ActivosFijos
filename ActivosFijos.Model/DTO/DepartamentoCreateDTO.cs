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
    public class DepartamentoCreateDTO
    {
        [Display(Name = "descripción")]
        public string Descripcion { get; set; }
    }
}
