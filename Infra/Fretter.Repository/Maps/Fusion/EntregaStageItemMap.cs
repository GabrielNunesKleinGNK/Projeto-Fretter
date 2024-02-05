using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageItemMap : IEntityTypeConfiguration<EntregaStageItem>
    {
        public void Configure(EntityTypeBuilder<EntregaStageItem> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageItem", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.EntregaStage).HasColumnName("Id_EntregaStage").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaEntrada).HasColumnName("Cd_EntregaEntrada").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.EntregaSaida).HasColumnName("Cd_EntregaSaida").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Sro).HasColumnName("Cd_Sro").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.SroReversa).HasColumnName("Cd_SroReversa").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Postagem).HasColumnName("Dt_Postagem").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PostagemReversa).HasColumnName("Dt_PostagemReversa").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PrevistaEntrega).HasColumnName("Dt_PrevistaEntrega").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.PrevistaEntregaReversa).HasColumnName("Dt_PrevistaEntregaReversa").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.Item).HasColumnName("Vl_Item").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.ItemTotal).HasColumnName("Vl_ItemTotal").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Frete).HasColumnName("Vl_Frete").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.FreteCobrado).HasColumnName("Vl_FreteCobrado").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.PostagemVerificada).HasColumnName("Flg_PostagemVerificada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Rastreada).HasColumnName("Flg_Rastreada").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Informacao).HasColumnName("Ds_Informacao").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.SoliticacaoReversa).HasColumnName("Dt_SoliticacaoReversa").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.ValidadeReversa).HasColumnName("Dt_ValidadeReversa").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.PendenteReversa).HasColumnName("Flg_PendenteReversa").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
        }
    }
}
