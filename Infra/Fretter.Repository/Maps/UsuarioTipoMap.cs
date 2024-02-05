using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class UsuarioTipoMap : IEntityTypeConfiguration<UsuarioTipo>
    {
        public void Configure(EntityTypeBuilder<UsuarioTipo> builder)
        {
            builder.ToTable("UsuarioTipo", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("UsuarioTipoId").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Descricao).HasColumnName("Descricao").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
        }
    }
}
