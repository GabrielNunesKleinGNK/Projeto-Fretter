using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaDevolucaoOcorrenciaMap : IEntityTypeConfiguration<EntregaDevolucaoOcorrencia>
    {
        public void Configure(EntityTypeBuilder<EntregaDevolucaoOcorrencia> builder)
        {
            builder.ToTable("Tb_Edi_EntregaDevolucaoOcorrencia", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaDevolucao).HasColumnName("Id_EntregaDevolucao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.OcorrenciaEmpresaItem).HasColumnName("Id_OcorrenciaEmpresaItem").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Sigla).HasColumnName("Nm_Sigla").HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.Observacao).HasColumnName("Ds_Observacao").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Ocorrencia).HasColumnName("Ds_Ocorrencia").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.DataOcorrencia).HasColumnName("Dt_Ocorrencia").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.OcorrenciaTipoId);
        }
    }
}
