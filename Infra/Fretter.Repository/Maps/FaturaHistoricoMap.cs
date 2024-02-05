using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class FaturaHistoricoMap : IEntityTypeConfiguration<FaturaHistorico>
    {
        public void Configure(EntityTypeBuilder<FaturaHistorico> builder)
        {
            builder.ToTable("FaturaHistorico", "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(FaturaHistorico)}Id").HasColumnType("int");
			builder.Property(e => e.FaturaId).HasColumnType("int");
            builder.Property(e => e.FaturaStatusId).HasColumnType("int");
            builder.Property(e => e.Descricao).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.ValorCustoFrete).HasColumnType("decimal");
            builder.Property(e => e.ValorCustoAdicional).HasColumnType("decimal");
            builder.Property(e => e.ValorCustoReal).HasColumnType("decimal");
            builder.Property(e => e.QuantidadeEntrega).HasColumnType("int");
            builder.Property(e => e.QuantidadeSucesso).HasColumnType("int");
            builder.Property(e => e.FaturaStatusAnteriorId).HasColumnName("FaturaStatusIdAnterior").HasColumnType("int");

            //BaseMapping
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.FaturaStatus).WithMany(x => x.FaturaHistorico).HasForeignKey(e => e.FaturaStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.FaturaStatusAnterior).WithMany().HasForeignKey(e => e.FaturaStatusAnteriorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
