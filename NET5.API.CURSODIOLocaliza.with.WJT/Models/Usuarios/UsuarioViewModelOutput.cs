using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Models.Usuarios
{
    public class UsuarioViewModelOutput
    {
        public int Codigo { get; internal set; }
        public string Login { get; internal set; }
        public string Email { get; internal set; }
    }
}
