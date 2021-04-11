## CONFIGURANDO ENTITY FRAMEWORK
-INCLUIR NUGET PACKAGE "Microsoft.EntityFrameworkCore", "Microsoft.EntityFrameworkCore.Relational", "Microsoft.EntityFrameworkCore.SqlServer", "Microsoft.EntityFrameworkCore.Tools"
-Adicionar sua classe de Context e herdar o DbContext do Entity
```
  public class CursoDbContext : DbContext
    {
    ...
```

-Implementar os objetos que serão mapeados das Entidades
```
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

```
