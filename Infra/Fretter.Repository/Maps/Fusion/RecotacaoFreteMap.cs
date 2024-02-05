using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class RecotacaoFreteMap : IEntityTypeConfiguration<RecotacaoFrete>
    {
        public void Configure(EntityTypeBuilder<RecotacaoFrete> builder)
        {
            builder.ToTable("Tb_MF_RecotacaoFrete", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.IdEmpresa).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.IdRecotacaoFreteStatus).HasColumnName("Id_RecotacaoFreteStatus").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.IdCanalVenda).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.IdRecotacaoFreteTipo).HasColumnName("Id_RecotacaoFreteTipo").HasColumnType("int").HasMaxLength(4);

			builder.Property(e => e.DsNomeArquivo).HasColumnName("Ds_NomeArquivo").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.DsUrl).HasColumnName("Ds_Url").HasColumnType("varchar").HasMaxLength(500);
			builder.Property(e => e.ObjJsonRetorno).HasColumnName("Ob_JsonRetorno").HasColumnType("varchar");
			builder.Property(e => e.QtAdvertencia).HasColumnName("Qt_Advertencia").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.QtErros).HasColumnName("Qt_Erros").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.DtInclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime");
			builder.Property(e => e.DtAtualizacao).HasColumnName("Dt_Atualizacao").HasColumnType("datetime");
			builder.Property(e => e.PriorizarPrazo).HasColumnName("Flg_PriorizarPrazo").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);


			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}