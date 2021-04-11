using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Repository;
using NET5.API.CURSODIOLocaliza.with.JWT.Models.Cursos;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Controllers
{
    [Route("api/v1/curso")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar um curso")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Curso curso = new Curso();
            curso.Nome = cursoViewModelInput.Nome;
            curso.Descricao = cursoViewModelInput.Descricao;
            curso.CodigoUsuario = CodigoUsuario;


            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            return Created("", cursoViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter os cursos")]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            //User é do Claim e quando criamos o authentication do usuario definimos o NameIdentifier como o codigo do usuario
            var CodigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
           
              var cursos =  _cursoRepository.ObterCursoPorUsuario(CodigoUsuario)
                .Select(s => new CursoViewModelOutput()
                {
                   Nome = s.Nome,
                   Descricao = s.Descricao,
                   Login = s.Usuario.Login
                });

            return Ok(cursos);
        }
    }
}
