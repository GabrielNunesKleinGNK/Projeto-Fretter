using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class IntegracaoMap : IEntityTypeConfiguration<Integracao>
    {
        public void Configure(EntityTypeBuilder<Integracao> builder)
        {
            builder.ToTable("Tb_Oco_Integracao", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.IntegracaoTipoId).HasColumnName("Id_IntegracaoTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaIntegracaoId).HasColumnName("Id_EmpresaIntegracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.URL).HasColumnName("Ds_URL").HasColumnType("varchar").HasMaxLength(1024);
            builder.Property(e => e.Verbo).HasColumnName("Ds_Verbo").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.LayoutHeader).HasColumnName("Ds_LayoutHeader").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Layout).HasColumnName("Ds_Layout").HasColumnType("varchar").HasMaxLength(2048);
            builder.Property(e => e.Registros).HasColumnName("Qtd_Registros").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Paralelo).HasColumnName("Qtd_Paralelo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EnvioConfigId).HasColumnName("Id_EnvioConfig").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Lote).HasColumnName("Flg_Lote").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.ProcessamentoSucesso).HasColumnName("Flg_ProcessamentoSucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Producao).HasColumnName("Flg_Producao").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EnvioBody).HasColumnName("Flg_EnvioBody").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EnviaOcorrenciaHistorico).HasColumnName("Flg_EnviaOcorrenciaHistorico").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.IntegracaoGatilho).HasColumnName("Flg_IntegracaoGatilho").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataProcessamento).HasColumnName("Dt_Processamento").HasColumnType("datetime").HasMaxLength(8);

            builder.HasOne(e => e.IntegracaoTipo).WithMany().HasForeignKey(e => e.IntegracaoTipoId).OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}