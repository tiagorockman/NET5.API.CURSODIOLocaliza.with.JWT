using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Business.Repository
{
   public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Commit();
        Usuario ObterUsuario(string login);
    }
}
