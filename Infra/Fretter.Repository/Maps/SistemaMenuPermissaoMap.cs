using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    public class SistemaMenuPermissaoMap : IEntityTypeConfiguration<SistemaMenuPermissao>
    {
        public void Configure(EntityTypeBuilder<SistemaMenuPermissao> builder)
        {
            builder.ToTable("SistemaMenuPermissao", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("SistemaMenuPermissaoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);


            builder.HasOne(e => e.SistemaMenu).WithMany().HasForeignKey(e => e.SistemaMenuId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Usuario).WithMany().HasForeignKey(e => e.UsuarioId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
