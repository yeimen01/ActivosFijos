using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model
{
    public class Respuesta
    {
        public Respuesta()
        {
            Code = 0;
        }

        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
