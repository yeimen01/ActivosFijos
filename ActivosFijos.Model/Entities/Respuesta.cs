using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.Entities
{
    public class Respuesta
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
