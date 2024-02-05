using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaMap : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
            builder.ToTable("Fatura", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(Fatura)}Id").HasColumnType("int");
			builder.Property(e => e.EmpresaId).HasColumnType("int");
            builder.Property(e => e.TransportadorId).HasColumnType("int").HasMaxLength(512);
            builder.Property(e => e.FaturaPeriodoId).HasColumnType("int").HasMaxLength(256);
            builder.Property(e => e.ValorCustoFrete).HasColumnType("decimal");
            builder.Property(e => e.ValorCustoAdicional).HasColumnType("decimal");
            builder.Property(e => e.ValorCustoReal).HasColumnType("decimal");
            builder.Property(e => e.ValorDocumento).HasColumnType("decimal");
            builder.Property(e => e.QuantidadeDevolvidoRemetente).HasColumnType("int");
            builder.Property(e => e.FaturaStatusId).HasColumnType("int").HasMaxLength(512);
            builder.Property(e => e.DataVencimento).HasColumnName("DataVencimento").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.QuantidadeSucesso).HasColumnType("int");
            builder.Property(e => e.QuantidadeEntrega).HasColumnType("int");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.FaturaStatus).WithMany(x => x.Fatura).HasForeignKey(e => e.FaturaStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.FaturaPeriodo).WithMany(x => x.Fatura).HasForeignKey(e => e.FaturaPeriodoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Transportador).WithMany().HasForeignKey(e => e.TransportadorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
