using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class SistemaMenuMap : IEntityTypeConfiguration<SistemaMenu>
    {
        public void Configure(EntityTypeBuilder<SistemaMenu> builder)
        {
            builder.ToTable("SistemaMenu", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("SistemaMenuId").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Descricao).HasColumnName("Descricao").HasColumnType("varchar").HasMaxLength(60);
            builder.Property(e => e.Icone).HasColumnName("Icone").HasColumnType("varchar").HasMaxLength(60);
            builder.Property(e => e.Rota).HasColumnName("Rota").HasColumnType("varchar").HasMaxLength(60);
            builder.Property(e => e.Ordem).HasColumnName("Ordem").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            //builder.HasQueryFilter(p => p.Ativo);
        }
    }
}
