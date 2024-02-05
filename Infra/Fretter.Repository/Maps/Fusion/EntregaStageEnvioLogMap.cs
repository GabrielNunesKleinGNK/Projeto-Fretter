using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps
{
    class EntregaStageEnvioLogMap : IEntityTypeConfiguration<EntregaStageEnvioLog>
    {
        public void Configure(EntityTypeBuilder<EntregaStageEnvioLog> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageEnvioLog", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaConfiguracaoId).HasColumnName("Id_EntregaConfiguracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.EntregaEntrada).HasColumnName("Cd_EntregaEntrada").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.EntregaSaida).HasColumnName("Cd_EntregaSaida").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Processamento).HasColumnName("Dt_Processamento").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Json).HasColumnName("Ds_Json").HasColumnType("varchar(MAX)");
            builder.Property(e => e.Retorno).HasColumnName("Ds_Retorno").HasColumnType("varchar(MAX)");
            builder.Property(e => e.Sucesso).HasColumnName("Flg_Sucesso").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Processado).HasColumnName("Flg_Processado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
