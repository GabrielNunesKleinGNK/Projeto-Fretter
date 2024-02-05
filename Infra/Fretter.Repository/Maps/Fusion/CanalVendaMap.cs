using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CanalVendaMap : IEntityTypeConfiguration<CanalVenda>
    {
        public void Configure(EntityTypeBuilder<CanalVenda> builder)
        {
            builder.ToTable("Tb_Adm_CanalVenda", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalVendaNome).HasColumnName("Ds_CanalVenda").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.Default).HasColumnName("Flg_Default").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            //builder.Property(e => e.CanalVendaUnico).HasColumnName("Ds_CanalVendaUnico").HasColumnType("varchar").HasMaxLength(200);
            //builder.Property(e => e.DefaultUnico).HasColumnName("Flg_DefaultUnico").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CanalVendaCodigo).HasColumnName("Cd_CanalVenda").HasColumnType("varchar").HasMaxLength(20);
            builder.Property(e => e.UltAtualizacaoProduto).HasColumnName("Dt_UltAtualizacaoProduto").HasColumnType("date").HasMaxLength(3);
            builder.Property(e => e.TipoIntegracao).HasColumnName("Id_TipoIntegracao").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.EmbalagemUnicaMF).HasColumnName("Flg_EmbalagemUnicaMF").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.OrigemImportacao).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);
        }
    }
}
