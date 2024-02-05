using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class IndicadorConciliacaoMap : IEntityTypeConfiguration<IndicadorConciliacao>
    {
        public void Configure(EntityTypeBuilder<IndicadorConciliacao> builder)
        {
            builder.ToTable("IndicadorConciliacao", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("IndicadorConciliacaoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Data).HasColumnName("Data").HasColumnType("date").HasMaxLength(3);
            builder.Property(e => e.EmpresaId).HasColumnName("EmpresaId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorId).HasColumnName("TransportadorId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorCnpjId).HasColumnName("TransportadorCnpjId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdEntrega).HasColumnName("QtdEntrega").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdCte).HasColumnName("QtdCte").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdSucesso).HasColumnName("QtdSucesso").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdErro).HasColumnName("QtdErro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdDivergencia).HasColumnName("QtdDivergencia").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdDivergenciaPeso).HasColumnName("QtdDivergenciaPeso").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtdDivergenciaTarifa).HasColumnName("QtdDivergenciaTarifa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ValorCustoFreteEstimado).HasColumnName("ValorCustoFreteEstimado").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.ValorCustoFreteReal).HasColumnName("ValorCustoFreteReal").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.ValorTarifaPesoEstimado).HasColumnName("ValorTarifaPesoEstimado").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.ValorTarifaPesoReal).HasColumnName("ValorTarifaPesoReal").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.DataProcessamento).HasColumnName("DataProcessamento").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
        }
    }
}
