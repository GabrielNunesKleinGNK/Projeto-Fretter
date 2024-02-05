using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoCteNotaFiscalMap : IEntityTypeConfiguration<ImportacaoCteNotaFiscal>
    {
        public void Configure(EntityTypeBuilder<ImportacaoCteNotaFiscal> builder)
        {
            builder.ToTable("ImportacaoCteNotaFiscal", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoCteNotaFiscal)}Id").HasColumnType("int");
            builder.Property(e => e.ImportacaoCteId).HasColumnType("int").IsRequired();
            builder.Property(e => e.Chave).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.DataPrevista).HasColumnType("datetime");
            builder.Property(e => e.CNPJEmissorNF).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.NumeroNF).HasColumnType("varchar").HasMaxLength(9);
            builder.Property(e => e.SerieNF).HasColumnType("varchar").HasMaxLength(3);
            builder.Property(e => e.DataEmissaoNF).HasColumnType("datetime");

            //BaseMapping
            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
