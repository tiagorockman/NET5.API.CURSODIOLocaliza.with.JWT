using Microsoft.EntityFrameworkCore;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;
using NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data
{
    public class CursoDbContext : DbContext
    {
        //Define as opcoes passadas para o contexto de conexao
        public CursoDbContext(DbContextOptions<CursoDbContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Curso> Curso { get; set; }

    }
}
