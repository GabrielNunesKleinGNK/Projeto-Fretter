using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoConfiguracaoTipoMap : IEntityTypeConfiguration<ImportacaoConfiguracaoTipo>
    {
        public void Configure(EntityTypeBuilder<ImportacaoConfiguracaoTipo> builder)
        {
            builder.ToTable("ImportacaoConfiguracaoTipo", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("ImportacaoConfiguracaoTipoId").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Nome).HasColumnName("Nome").HasColumnType("varchar").HasMaxLength(32);
			builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.DataCadastro)
                .Ignore(e => e.UsuarioCadastro)
                .Ignore(e => e.DataAlteracao)
                .Ignore(e => e.UsuarioAlteracao);
        }
    }
}
