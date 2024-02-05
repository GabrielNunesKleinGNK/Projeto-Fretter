using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaPeriodoMap : IEntityTypeConfiguration<FaturaPeriodo>
    {
        public void Configure(EntityTypeBuilder<FaturaPeriodo> builder)
        {
            builder.ToTable("FaturaPeriodo", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaPeriodo)}Id").HasColumnType("int");
            builder.Property(e => e.FaturaCicloId).HasColumnType("int");
            builder.Property(e => e.DiaVencimento).HasColumnType("smallint");
            builder.Property(e => e.DataInicio).HasColumnType("datetime");
            builder.Property(e => e.DataFim).HasColumnType("datetime");
            builder.Property(e => e.Vigente).HasColumnType("bit");
            builder.Property(e => e.Processado).HasColumnType("bit");
            builder.Property(e => e.DataProcessamento).HasColumnType("datetime");
            builder.Property(e => e.QuantidadeProcessado).HasColumnType("int");
            builder.Property(e => e.DuracaoProcessamento).HasColumnType("int");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            //builder.HasOne(e => e.Fatura).WithMany(x => x.).HasForeignKey(e => e.FaturaId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
