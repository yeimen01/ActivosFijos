using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivosFijos.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [RegularExpression(@"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.\W)[a-zA-Z\d\W]{8,}$", ErrorMessage = Utilities.Utilities.MsgPassword)]
        public string Password { get; set; }
    }
}
