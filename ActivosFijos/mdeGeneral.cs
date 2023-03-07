using ActivosFijos.Model;
using System.Net;

namespace ActivosFijos
{
    public static class MG
    {
        public const string MsgRequired = "El campo {0} es requerido";
        public const string MsgInputRequired = "El campo {0} es requerido, debe seleccionar un valor valido.";
        public const string MsgMaxLetters = "El campo {0} es debe tener un maximo de {1} caracteres.";
        public const string MsgMinLetters = "El campo {0} es debe tener un minimo {1} caracteres.";
        public const string MsgCedula = "El campo {0} solo admite letras y números sin espacios o separadores.";
        public const string MsgEmailAddress = "El campo {0} no es un correo electrónico valido, ponga uno valido, por favor.";

        public static Respuesta Respuesta(HttpStatusCode code, string message, object data = null)
        {
            Respuesta response = new Respuesta
            {
                Code= code,
                Message= message,
                Data = data
            };

            return response;
        }
    }
}
