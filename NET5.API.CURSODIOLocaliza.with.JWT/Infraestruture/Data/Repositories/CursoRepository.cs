using Microsoft.EntityFrameworkCore;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Repository;
using System.Collections.Generic;
using System.Linq;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _contexto;

        public CursoRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
        }
        public void Adicionar(Curso curso)
        {
            _contexto.Curso.Add(curso);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public List<Curso> ObterCursoPorUsuario(int codigoUsuario)
        {
            //o include faz um inner join
            return _contexto.Curso.Include(i => i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}
