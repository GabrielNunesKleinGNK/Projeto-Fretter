using Fretter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretter.Repository.Maps
{
    class TabelasCorreiosArquivoMap : IEntityTypeConfiguration<TabelasCorreiosArquivo>
    {
        public void Configure(EntityTypeBuilder<TabelasCorreiosArquivo> builder)
        {
            builder.ToTable("Tb_MFC_TabelasCorreiosArquivo", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.TabelaArquivoStatusId).HasColumnName("Id_TabelaArquivoStatus").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.NomeArquivo).HasColumnName("Ds_NomeArquivo").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.Url).HasColumnName("Ds_Url").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(e => e.Criacao).HasColumnName("Dt_Criacao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.ImportacaoDados).HasColumnName("Dt_ImportacaoDados").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.AtualizacaoTabelas).HasColumnName("Dt_AtualizacaoTabelas").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.Erro).HasColumnName("Ds_Erro").HasColumnType("varchar").HasMaxLength(4096);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.HasOne(x => x.TabelaArquivoStatus).WithMany().HasForeignKey(x => x.TabelaArquivoStatusId).OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.UsuarioCadastro);
        }
    }
}
