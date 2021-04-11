using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Models
{
    public class ValidaCampoModel
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidaCampoModel(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
