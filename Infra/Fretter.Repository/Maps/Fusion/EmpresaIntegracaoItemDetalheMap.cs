using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps.Fusion
{
    internal class EmpresaIntegracaoItemDetalheMap : IEntityTypeConfiguration<EmpresaIntegracaoItemDetalhe>
    {
        public void Configure(EntityTypeBuilder<EmpresaIntegracaoItemDetalhe> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaIntegracaoItemDetalhe", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);

            builder.Property(e => e.EmpresaIntegracaoItemId).HasColumnName("Id_EmpresaIntegracaoItem").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("bigint").HasMaxLength(8);
            builder.Property(e => e.Chave).HasColumnName("Cd_Chave").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.RequestURL).HasColumnName("Ds_RequestURL").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.JsonEnvio).HasColumnName("Ds_JsonEnvio").HasColumnType("varchar").HasMaxLength(4096);
            builder.Property(e => e.JsonBody).HasColumnName("Ds_JsonBody").HasColumnType("varchar").HasMaxLength(int.MaxValue);
            builder.Property(e => e.JsonRetorno).HasColumnName("Ds_JsonRetorno").HasColumnType("varchar").HasMaxLength(6144);
            builder.Property(e => e.HttpTempo).HasColumnName("Ds_HttpTempo").HasColumnType("bigint").HasMaxLength(8);
            builder.Property(e => e.HttpStatus).HasColumnName("Ds_HttpStatus").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.HttpResponse).HasColumnName("Ds_HttpResponse").HasColumnType("varchar").HasMaxLength(4096);
            builder.Property(e => e.Sucesso).HasColumnName("Flg_Sucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.PendenteProcessamentoRetorno).HasColumnName("Flg_PendenteProcessamentoRetorno").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.ProcessadoRetornoSucesso).HasColumnName("Flg_ProcessadoRetornoSucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.MensagemRetorno).HasColumnName("Ds_MensagemRetorno").HasColumnType("varchar").HasMaxLength(4096);
            builder.Property(e => e.HttpStatusCode).HasColumnName("Nr_HttpStatus").HasColumnType("int").HasMaxLength(1);

            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Cadastro").HasColumnType("datetime").HasMaxLength(8);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
