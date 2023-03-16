using ActivosFijos.Model.Entities;
using ActivosFijos.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;


namespace ActivosFijos.Model.Utilities
{
    public static class Utilities
    {
        public const string MsgRequired = "El campo {0} es requerido";
        public const string MsgInputRequired = "El campo {0} es requerido, debe seleccionar un valor valido.";
        public const string MsgMaxLetters = "El campo {0} es debe tener un maximo de {1} caracteres.";
        public const string MsgMinLetters = "El campo {0} es debe tener un minimo {1} caracteres.";
        public const string MsgCedula = "El campo {0} solo admite letras y números sin espacios o separadores.";
        public const string MsgEmailAddress = "El campo {0} no es un correo electrónico valido, ponga uno valido, por favor.";
        public const string IdNotFound = "No se encontró ningún registro del Id proporcionado.";
        public const string NotFound = "No se encontró ningún registro.";

        public static Respuesta Respuesta(HttpStatusCode code, string message, Object data = null)
        {
            Respuesta respuesta = new Respuesta
            {
                Code = code,
                Message = message,
                Data= data
            };

            return respuesta;
        }

        public static ObjectResult RespuestaActionResult(Respuesta respuesta)
        {
            var response = new ObjectResult(respuesta)
            {
                StatusCode = (int?)respuesta.Code
            };

            return response;
        }

        public static string TipoPersona(TipoPersona TipoPersona)
        {
            string persona = "";

            switch (TipoPersona)
            {
                case TipoPersona.Fisica:
                    persona = "Física";
                break;

                case TipoPersona.Juridica:
                    persona = "Jurídica";
                break;
            }

            return persona;
        }

        public static string Estado(Estado Estado)
        {
            string estado = "";

            switch (Estado)
            {
                case Estado.Pendiente:
                    estado = "Pendiente";
                    break;

                case Estado.Registrado:
                    estado = "Registrado";
                    break;
            }

            return estado;
        }

        public static string TipoMovimiento(TipoMovimiento TipoMovimiento)
        {
            string tipoMovimiento = "";

            switch (TipoMovimiento)
            {
                case TipoMovimiento.Debito:
                    tipoMovimiento = "Débito";
                    break;

                case TipoMovimiento.Credito:
                    tipoMovimiento = "Crédito";
                    break;
            }

            return tipoMovimiento;
        }

    }
}
