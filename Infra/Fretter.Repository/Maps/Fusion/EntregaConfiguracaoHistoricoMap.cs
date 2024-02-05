using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaConfiguracaoHistoricoMap : IEntityTypeConfiguration<EntregaConfiguracaoHistorico>
    {
        public void Configure(EntityTypeBuilder<EntregaConfiguracaoHistorico> builder)
        {
            builder.ToTable("Tb_Edi_EntregaConfiguracaoHistorico", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("bigint").HasMaxLength(8);
			builder.Property(e => e.EntregaConfiguracaoId).HasColumnName("Id_EntregaConfiguracao").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Processado).HasColumnName("Qtd_Processado").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaMinima).HasColumnName("Dt_EntregaMinima").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.EntregaMaxima).HasColumnName("Dt_EntregaMaxima").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.PeriodoInicial).HasColumnName("Dt_PeriodoInicial").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.PeriodoFinal).HasColumnName("Dt_PeriodoFinal").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.ControleInicial).HasColumnName("Nr_ControleInicial").HasColumnType("bigint").HasMaxLength(8);
			builder.Property(e => e.ControleFinal).HasColumnName("Nr_ControleFinal").HasColumnType("bigint").HasMaxLength(8);
			builder.Property(e => e.MensagemRetorno).HasColumnName("Ds_MensagemRetorno").HasColumnType("varchar").HasMaxLength(2048);
			builder.Property(e => e.Sucesso).HasColumnName("Flg_Sucesso").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

			builder.Ignore(x => x.DataAlteracao); 
			builder.Ignore(x => x.DataCadastro);
			builder.Ignore(x => x.UsuarioCadastro);
			builder.Ignore(x => x.UsuarioAlteracao);

			builder.HasOne(x => x.EntregaConfiguracao).WithMany(x => x.Historicos).HasForeignKey(x => x.EntregaConfiguracaoId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
