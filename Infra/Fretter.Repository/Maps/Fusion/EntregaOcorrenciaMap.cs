using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaOcorrenciaMap : IEntityTypeConfiguration<EntregaOcorrencia>
    {
        public void Configure(EntityTypeBuilder<EntregaOcorrencia> builder)
        {
            builder.ToTable("Tb_Edi_EntregaOcorrencia", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.EntregaId).HasColumnName("Id_Entrega").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.OcorrenciaId).HasColumnName("Id_Ocorrencia").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Ocorrencia).HasColumnName("Ds_Ocorrencia").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.DataOcorrencia).HasColumnName("Dt_Ocorrencia").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.DataInclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.DataOriginal).HasColumnName("Dt_Original").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.OcorrenciaValidada).HasColumnName("Flg_OcorrenciaValidada").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EdiId).HasColumnName("Id_Edi").HasColumnType("uniqueidentifier").HasMaxLength(16);
            builder.Property(e => e.ArquivoImportacao).HasColumnName("Ds_ArquivoImportacao").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.UsuarioInclusao).HasColumnName("Ds_UsuarioInclusao").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.OcorrenciaValidadaDe).HasColumnName("Flg_OcorrenciaValidadaDe").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.OcorrenciaDeId).HasColumnName("Id_OcorrenciaDe").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.OrigemImportacaoId).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.DataInclusaoAnterior).HasColumnName("Dt_InclusaoAnterior").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.DataOcorrenciaAnterior).HasColumnName("Dt_OcorrenciaAnterior").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.CodigoBaseTransportador).HasColumnName("Cd_BaseTransportador").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.Complementar).HasColumnName("Ds_Complementar").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(e => e.ArquivoId).HasColumnName("Id_Arquivo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataPostagemAtualiza).HasColumnName("Dt_PostagemAtualiza").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.Sigla).HasColumnName("Nm_Sigla").HasColumnType("varchar").HasMaxLength(5);
            builder.Property(e => e.Finalizar).HasColumnName("Tp_Finalizar").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Dominio).HasColumnName("Ds_Dominio").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Cidade).HasColumnName("Nm_Cidade").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Uf).HasColumnName("Cd_Uf").HasColumnType("char").HasMaxLength(2);
            builder.Property(e => e.Latitude).HasColumnName("Cd_Latitude").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Longitude).HasColumnName("Cd_Longitude").HasColumnType("varchar").HasMaxLength(128);


            builder.Ignore(e => e.Ativo);
			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}