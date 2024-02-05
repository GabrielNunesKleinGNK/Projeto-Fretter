using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ContratoTransportadorRegraMap : IEntityTypeConfiguration<ContratoTransportadorRegra>
    {
        public void Configure(EntityTypeBuilder<ContratoTransportadorRegra> builder)
        {
            builder.ToTable("ContratoTransportadorRegra", "Fretter");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ContratoTransportadorRegraId").HasColumnType("int").IsRequired(true);
            builder.Property(e => e.TransportadorId).HasColumnName("TransportadorId").HasColumnType("int").IsRequired(true);
            builder.Property(e => e.OcorrenciaId).HasColumnName("OcorrenciaEmpresaItemId").HasColumnType("int").IsRequired(true);
            builder.Property(e => e.EmpresaTransportadorConfigId).HasColumnName("EmpresaTransportadorConfigId").HasColumnType("int").IsRequired(false);
            builder.Property(e => e.TipoCondicao).HasColumnName("ContratoTransportadorRegraTipoId").HasColumnType("tinyint").IsRequired(true);
            builder.Property(e => e.Operacao).HasColumnName("Acrescimo").HasColumnType("bit").IsRequired(true);
            builder.Property(e => e.Valor).HasColumnName("Valor").HasColumnType("decimal").HasPrecision(10, 4).IsRequired(true);
            builder.Property(e => e.ConciliacaoTipoId).HasColumnName("ConciliacaoTipoId").HasColumnType("int");

            builder.Ignore(e => e.Ocorrencia);
        }
    }
}
