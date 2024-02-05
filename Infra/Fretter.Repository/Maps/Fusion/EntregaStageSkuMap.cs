using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageSkuMap : IEntityTypeConfiguration<EntregaStageSku>
    {
        public void Configure(EntityTypeBuilder<EntregaStageSku> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageSku", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaStageItem).HasColumnName("Id_EntregaStageItem").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DescricaoItem).HasColumnName("Ds_Item").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Sku).HasColumnName("Cd_Sku").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.ValorProduto).HasColumnName("Vl_Produto").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.ValorProdutoUnitario).HasColumnName("Vl_ProdutoUnitario").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Altura).HasColumnName("Vl_Altura").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Comprimento).HasColumnName("Vl_Comprimento").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Largura).HasColumnName("Vl_Largura").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Peso).HasColumnName("Vl_Peso").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Cubagem).HasColumnName("Vl_Cubagem").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Diametro).HasColumnName("Vl_Diametro").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Quantidade).HasColumnName("Vl_Quantidade").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
        }
    }
}
