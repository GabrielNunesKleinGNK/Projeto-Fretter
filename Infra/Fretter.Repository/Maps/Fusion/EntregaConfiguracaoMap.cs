using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaConfiguracaoMap : IEntityTypeConfiguration<EntregaConfiguracao>
    {
        public void Configure(EntityTypeBuilder<EntregaConfiguracao> builder)
        {
            builder.ToTable("Tb_Edi_EntregaConfiguracao", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaConfiguracaoTipo).HasColumnName("Id_EntregaConfiguracaoTipo").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Caminho).HasColumnName("Ds_Caminho").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Verbo).HasColumnName("Ds_Verbo").HasColumnType("varchar").HasMaxLength(32);
			builder.Property(e => e.Layout).HasColumnName("Ds_Layout").HasColumnType("varchar").HasMaxLength(1024);
			builder.Property(e => e.LayoutHeader).HasColumnName("Ds_LayoutHeader").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.ApiKey).HasColumnName("Ds_ApiKey").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.URLStageCallBack).HasColumnName("Ds_URLStageCallBack").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.URLEtiquetaCallBack).HasColumnName("Ds_URLEtiquetaCallBack").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Lote).HasColumnName("Flg_Lote").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Registro).HasColumnName("Qtd_Registro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Paralelo).HasColumnName("Qtd_Paralelo").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.ProcessamentoSucesso).HasColumnName("Flg_ProcessamentoSucesso").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.DataProcessamento).HasColumnName("Dt_Processamento").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.DataProximaExecucao).HasColumnName("Dt_ProximaExecucao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.IntervaloExecucao).HasColumnName("Cd_IntervaloExecucao").HasColumnType("smallint").HasMaxLength(2);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.IntervaloExecucaoTipo).HasColumnName("Cd_IntervaloExecucaoTipo").HasColumnType("smallint").HasMaxLength(2);

			builder.HasMany(e => e.Itens).WithOne().HasForeignKey(e => e.EntregaConfiguracaoId).OnDelete(DeleteBehavior.Restrict);

			builder.Ignore(x => x.DataAlteracao);
			builder.Ignore(x => x.DataCadastro);
			builder.Ignore(x => x.UsuarioCadastro);
			builder.Ignore(x => x.UsuarioAlteracao);
		}
    }
}
