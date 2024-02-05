using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoCteCargaMap : IEntityTypeConfiguration<ImportacaoCteCarga>
    {
        public void Configure(EntityTypeBuilder<ImportacaoCteCarga> builder)
        {
            builder.ToTable("ImportacaoCteCarga", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoCteCarga)}Id").HasColumnType("int");
            builder.Property(e => e.ImportacaoCteId).HasColumnType("int").IsRequired();
            builder.Property(e => e.Tipo).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Codigo).HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Quantidade).HasColumnType("decimal(10,4)");
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
