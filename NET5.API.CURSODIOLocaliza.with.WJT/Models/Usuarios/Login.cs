using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Models.Usuarios
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O Login é obrigatório.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
