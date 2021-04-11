using Microsoft.AspNetCore.Mvc;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Repository;
using NET5.API.CURSODIOLocaliza.with.JWT.Configurations;
using NET5.API.CURSODIOLocaliza.with.JWT.Filters;
using NET5.API.CURSODIOLocaliza.with.JWT.Models;
using NET5.API.CURSODIOLocaliza.with.JWT.Models.Usuarios;
using Swashbuckle.AspNetCore.Annotations;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        //inversão de controle
        private readonly IUsuarioRepository _usuarioRepository;
       
        private readonly IAuthenticationServiceCustom _authenticationService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IAuthenticationServiceCustom authenticationService)
        {
            _usuarioRepository = usuarioRepository;            
            _authenticationService = authenticationService;
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginModel))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado] //A anotação dessa classe substitui o ModelState.IsValid
        public IActionResult Logar(LoginModel loginViewInput)
        {
            #region Validate Model State foi inutilizado devido a classe ValidacaoModelStateCustomizado
            //if (!ModelState.IsValid)
            //    return BadRequest(
            //        new ValidaCampoModel(
            //        ModelState.SelectMany(sm=>sm.Value.Errors)
            //        .Select(s => s.ErrorMessage)
            //        )); // se os dados passados no paramtro estiverem incorretos retorna uma instancia de ValidaCampoModel ou seja preenchendo o retorno com o erro

            #endregion

            Usuario usuario = _usuarioRepository.ObterUsuario(loginViewInput.Login);

            if (usuario == null)
                return BadRequest("Usuário inexistente ou houve um erro ao tentar acessar");

            //if(usuario.Senha != loginModel.Senha.GerarSenhaCriptografada())
            //{
            //    return BadRequest("Houve um erro ao tentar acessar.");
            //}

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewInput.Login,
                Email = usuario.Email,
            };

            string token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }


        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoModel))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(LoginViewModelInput loginInput)
        {

            //var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CursoNet5Api;Integrated Security=True");
            //CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            //var migracoesPendentes = contexto.Database.GetPendingMigrations(); //Verifica se existe migrações pendentes
            //if(migracoesPendentes.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}
            var usuario = new Usuario();
            usuario.Login = loginInput.Login;
            usuario.Senha = loginInput.Senha;
            usuario.Email = loginInput.Email;

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", loginInput);
        }
    }
}
