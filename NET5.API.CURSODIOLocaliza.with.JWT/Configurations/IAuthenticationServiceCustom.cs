using NET5.API.CURSODIOLocaliza.with.JWT.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Configurations
{
   public interface IAuthenticationServiceCustom
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}
