using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaConciliacaoMap : IEntityTypeConfiguration<FaturaConciliacao>
    {
        public void Configure(EntityTypeBuilder<FaturaConciliacao> builder)
        {
            builder.ToTable("FaturaConciliacao", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaConciliacao)}Id").HasColumnType("int");
            builder.Property(e => e.FaturaId).HasColumnType("int");
            builder.Property(e => e.ConciliacaoId).HasColumnType("bigint");
            builder.Property(e => e.Cnpj).HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.NotaFiscal).HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Serie).HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.ValorFrete).HasColumnType("decimal(10, 4)");
            builder.Property(e => e.ValorAdicional).HasColumnType("decimal(10, 4)");
            builder.Property(e => e.Observacao).HasColumnType("varchar").HasMaxLength(25);
            builder.Property(e => e.DataEmissao).HasColumnName("DataVencimento").HasColumnType("datetime").HasMaxLength(8);

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.Fatura).WithMany(x => x.FaturaConciliacoes).HasForeignKey(e => e.FaturaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Conciliacao).WithMany(x => x.FaturaConciliacoes).HasForeignKey(e => e.ConciliacaoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
