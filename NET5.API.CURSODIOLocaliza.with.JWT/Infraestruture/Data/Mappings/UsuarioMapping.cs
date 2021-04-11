using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NET5.API.CURSODIOLocaliza.with.JWT.Business.Entities;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Infraestruture.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO"); //Nome da Tabela
            builder.HasKey(p => p.Codigo); //Chave Primaria
            builder.Property(p => p.Codigo).ValueGeneratedOnAdd(); //AutoIncremento
            builder.Property(p => p.Senha); //outras colunas da tabela
            builder.Property(p => p.Email);

        }
    }
}
