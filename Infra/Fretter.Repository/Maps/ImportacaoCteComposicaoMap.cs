using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoCteComposicaoMap : IEntityTypeConfiguration<ImportacaoCteComposicao>
    {
        public void Configure(EntityTypeBuilder<ImportacaoCteComposicao> builder)
        {
            builder.ToTable("ImportacaoCteComposicao", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoCteComposicao)}Id").HasColumnType("int");
            builder.Property(e => e.ImportacaoCteId).HasColumnType("int").IsRequired();
            builder.Property(e => e.Nome).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Valor).HasColumnType("decimal(8,2)");
            builder.Property(e => e.ConfiguracaoCteTipoId).HasColumnType("int");

            //BaseMapping
            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

        }
    }
}
