using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ImportacaoCteImpostoMap : IEntityTypeConfiguration<ImportacaoCteImposto>
    {
        public void Configure(EntityTypeBuilder<ImportacaoCteImposto> builder)
        {
            builder.ToTable("ImportacaoCteImposto", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ImportacaoCteImposto)}Id").HasColumnType("int");
            builder.Property(e => e.Classificacao).HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.ValorBaseCalculo).HasColumnType("decimal(20,4)");
            builder.Property(e => e.Aliquota).HasColumnType("decimal(12,4)");
            builder.Property(e => e.Valor).HasColumnType("decimal(12,4)");            

            //BaseMapping
            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

        }
    }
}
