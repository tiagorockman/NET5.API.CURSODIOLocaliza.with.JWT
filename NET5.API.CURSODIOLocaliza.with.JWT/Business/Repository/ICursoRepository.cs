using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using System.Collections.Generic;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Business.Repository
{
    public interface ICursoRepository
    {
        void Adicionar(Curso curso);
        void Commit();
        List<Curso> ObterCursoPorUsuario(int codigoUsuario);
    }
}
