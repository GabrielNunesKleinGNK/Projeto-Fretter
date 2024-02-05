using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaConciliacaoMap : IEntityTypeConfiguration<EntregaConciliacao>
    {
        public void Configure(EntityTypeBuilder<EntregaConciliacao> builder)
        {
            builder.ToTable("Tb_Edi_EntregaConciliacao", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("bigint").HasMaxLength(8);
            builder.Property(e => e.EntregaId).HasColumnName("Cd_Id").HasColumnType("bigint").HasMaxLength(8);
            builder.Property(e => e.ValorCobrado).HasColumnName("Vl_Cobrado").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Altura).HasColumnName("Vl_Altura").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Largura).HasColumnName("Vl_Largura").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Comprimento).HasColumnName("Vl_Comprimento").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Peso).HasColumnName("Vl_Peso").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Cubagem).HasColumnName("Vl_Cubagem").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Diametro).HasColumnName("Vl_Diametro").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.Cubagem).HasColumnName("Vl_Cubagem").HasColumnType("decimal").HasMaxLength(9);
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DataProcessamento).HasColumnName("Dt_Processamento").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.RetornoProcessamento).HasColumnName("Ds_RetornoProcessamento").HasColumnType("varchar").HasMaxLength(2048);
            builder.Property(e => e.Processado).HasColumnName("Flg_Processado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Sucesso).HasColumnName("Flg_Sucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.UsuarioCadastro);
        }
    }
}
