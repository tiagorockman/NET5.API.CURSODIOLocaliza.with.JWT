using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NET5.API.CURSODIOLocaliza.with.JWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Filters
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validaCampoModel = new ValidaCampoModel(
                    context.ModelState.SelectMany(sm => sm.Value.Errors)
                    .Select(s => s.ErrorMessage)
                    ); // se os dados passados no paramtro estiverem incorretos retorna uma instancia de ValidaCampoModel ou seja preenchendo o retorno com o erro
                context.Result = new BadRequestObjectResult(validaCampoModel);

            }
        }
    }
}
