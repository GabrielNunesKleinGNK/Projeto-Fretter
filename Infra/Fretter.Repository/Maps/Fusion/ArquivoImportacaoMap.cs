using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ArquivoImportacaoMap : IEntityTypeConfiguration<ArquivoImportacao>
    {
        public void Configure(EntityTypeBuilder<ArquivoImportacao> builder)
        {
            builder.ToTable("Tb_Edi_ArquivoImportacao", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DtImportacao).HasColumnName("Dt_Importacao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.DsNome).HasColumnName("Ds_Nome").HasColumnType("varchar");
            builder.Property(e => e.DtTratamento).HasColumnName("Dt_Tratamento").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.FlgTratamento).HasColumnName("Flg_Tratamento").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DsArquivo).HasColumnName("Ds_Arquivo").HasColumnType("varbinary");
            builder.Property(e => e.OrigemId).HasColumnName("Origem_Id").HasColumnType("tinyint").HasMaxLength(4);
            builder.Property(e => e.IdArquivoImportacaoPai).HasColumnName("Id_ArquivoImportacao_Pai").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DtImportacaoDATE).HasColumnName("Dt_ImportacaoDATE").HasColumnType("date").HasMaxLength(4);
            builder.Property(e => e.FlComprimido).HasColumnName("Fl_Comprimido").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DsExtensao).HasColumnName("Ds_Extensao").HasColumnType("varchar");
            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
