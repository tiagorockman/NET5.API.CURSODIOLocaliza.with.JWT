using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //Inversão de Controle
       private readonly CursoDbContext _contexto;

        //Injeção de Dependencia
        // Ja espera na instancia de userRepository o Contexto
        public UsuarioRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Usuario usuario)
        {
            _contexto.Usuario.Add(usuario);
            
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public Usuario ObterUsuario(string login)
        {
            return _contexto.Usuario.FirstOrDefault(u => u.Login == login);
        }
    }
}
