using ActivosFijos.Model;
using ActivosFijos.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class DepartamentoUpdateDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public Estado Estado { get; set; }
    }
}
