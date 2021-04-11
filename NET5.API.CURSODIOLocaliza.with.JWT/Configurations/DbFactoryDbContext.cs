using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            //Conecta na base cria uma instancia e retorna o contexto
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=Curso;user=sa;passwdord=1234");
            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
