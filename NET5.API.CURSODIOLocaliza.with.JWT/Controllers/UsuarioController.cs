using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using NET5.API.CURSODIOLocaliza.with.JWT.Filters;
using NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data;
using NET5.API.CURSODIOLocaliza.with.JWT.Models;
using NET5.API.CURSODIOLocaliza.with.JWT.Models.Usuarios;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginModel))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado] //A anotação dessa classe substitui o ModelState.IsValid
        public IActionResult Logar(LoginModel loginViewInput)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(
            //        new ValidaCampoModel(
            //        ModelState.SelectMany(sm=>sm.Value.Errors)
            //        .Select(s => s.ErrorMessage)
            //        )); // se os dados passados no paramtro estiverem incorretos retorna uma instancia de ValidaCampoModel ou seja preenchendo o retorno com o erro

            //login Fake
            var userViewModel = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "james",
                Email = "james@email.com"
            };

            //Validação JWT
            var secret = Encoding.ASCII.GetBytes("esta é minha chave secreta para a autenticação");
            var symetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userViewModel.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, userViewModel.Login),
                    new Claim(ClaimTypes.Email, userViewModel.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            }; // Configura o Token

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new
            {
                Token = token,
                Usuario = userViewModel
            });
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(LoginViewModelInput loginInput)
        {

            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CursoNet5Api;Integrated Security=True");
            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            var migracoesPendentes = contexto.Database.GetPendingMigrations(); //Verifica se existe migrações pendentes
            if(migracoesPendentes.Count() > 0)
            {
                contexto.Database.Migrate();
            }
            var usuario = new Usuario();
            usuario.Login = loginInput.Login;
            usuario.Senha = loginInput.Senha;
            usuario.Email = loginInput.Email;

            //contexto.Usuario.Add(usuario);
            //contexto.SaveChanges();

            return Created("", loginInput);
        }
    }
}
