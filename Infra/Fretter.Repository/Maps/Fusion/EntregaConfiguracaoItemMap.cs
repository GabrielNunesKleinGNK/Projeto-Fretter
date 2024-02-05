using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaConfiguracaoItemMap : IEntityTypeConfiguration<EntregaConfiguracaoItem>
    {
        public void Configure(EntityTypeBuilder<EntregaConfiguracaoItem> builder)
        {
            builder.ToTable("Tb_Edi_EntregaConfiguracaoItem", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaConfiguracaoId).HasColumnName("Id_EntregaConfiguracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaConfiguracaoItemTipoId).HasColumnName("Id_EntregaConfiguracaoItemTipo").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Url).HasColumnName("Ds_URL").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Verbo).HasColumnName("Ds_Verbo").HasColumnType("varchar").HasMaxLength(8);
			builder.Property(e => e.Layout).HasColumnName("Ds_Layout").HasColumnType("varchar").HasMaxLength(2048);
			builder.Property(e => e.LayoutHeader).HasColumnName("Ds_LayoutHeader").HasColumnType("varchar").HasMaxLength(2048);
			builder.Property(e => e.ProcessadoSucesso).HasColumnName("Flg_ProcessadoSucesso").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.DataProcessamento).HasColumnName("Dt_Processamento").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

			builder.Ignore(x => x.DataAlteracao);
			builder.Ignore(x => x.DataCadastro);
			builder.Ignore(x => x.UsuarioCadastro);
			builder.Ignore(x => x.UsuarioAlteracao);
		}
    }
}
