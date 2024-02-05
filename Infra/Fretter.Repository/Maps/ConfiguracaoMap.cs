using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ConfiguracaoMap : IEntityTypeConfiguration<Configuracao>
    {
        public void Configure(EntityTypeBuilder<Configuracao> builder)
        {
            builder.ToTable("Configuracao", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("ConfiguracaoId").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Chave).HasColumnName("Chave").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.Valor).HasColumnName("Valor").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
        }
    }
}
