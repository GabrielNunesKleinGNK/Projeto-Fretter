using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class OcorrenciaArquivoMap : IEntityTypeConfiguration<OcorrenciaArquivo>
    {
        public void Configure(EntityTypeBuilder<OcorrenciaArquivo> builder)
        {
            builder.ToTable("Tb_Edi_OcorrenciaArquivo", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TabelaArquivoStatusId).HasColumnName("Id_TabelaArquivoStatus").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.NomeArquivo).HasColumnName("Ds_NomeArquivo").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.Url).HasColumnName("Ds_Url").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(e => e.DataCadastro).HasColumnName("Dt_Criacao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Retorno).HasColumnName("Ds_Retorno").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Atualizacao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.QtAdvertencia).HasColumnName("Qt_Advertencia").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtErros).HasColumnName("Qt_Erros").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.QtRegistros).HasColumnName("Qt_Registros").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.PercentualAtualizacao).HasColumnName("Nr_PercentualAtualizacao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UltimaAtualizacao).HasColumnName("Dt_UltimaAtualizacao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Usuario).HasColumnName("Nm_Usuario").HasColumnType("varchar").HasMaxLength(128);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.Ativo);
        }
    }
}
