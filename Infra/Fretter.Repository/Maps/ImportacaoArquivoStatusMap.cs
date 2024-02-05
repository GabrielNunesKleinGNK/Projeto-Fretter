using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoArquivoStatusMap : IEntityTypeConfiguration<ImportacaoArquivoStatus>
    {
        public void Configure(EntityTypeBuilder<ImportacaoArquivoStatus> builder)
        {
            builder.ToTable("ImportacaoArquivoStatus", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoArquivoStatus)}Id").HasColumnType("int");
			builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(128);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
        }
    }
}
