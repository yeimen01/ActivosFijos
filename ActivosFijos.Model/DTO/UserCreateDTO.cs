using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.DTO
{
    public class UserCreateDTO
    {
        public string Username { get; set; }

        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^\w\s]).{8,}$",
            ErrorMessage = "La contraseña debe contener al menos un número, una letra minúscula, una letra mayúscula, un carácter no alfanumérico, y tener una longitud mínima de 8 caracteres")]
        public string Password { get; set; }
    }
}
