using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageEntradaMap : IEntityTypeConfiguration<EntregaStageEntrada>
    {
        public void Configure(EntityTypeBuilder<EntregaStageEntrada> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageEntrada", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Hash).HasColumnName("Ds_Hash").HasColumnType("uniqueidentifier").HasMaxLength(16);
			builder.Property(e => e.EntregaSaida).HasColumnName("Cd_EntregaSaida").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Json).HasColumnName("Ds_Json").HasColumnType("varchar(max)");
			builder.Property(e => e.Validada).HasColumnName("Flg_Validada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.EntregaStage).HasColumnName("Id_EntregaStage").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
        }
    }
}
