using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaTransportadorConfigMap : IEntityTypeConfiguration<EmpresaTransportadorConfig>
    {
        public void Configure(EntityTypeBuilder<EmpresaTransportadorConfig> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaTransportadorConfig", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.PrazoComercial).HasColumnName("Nr_PrazoComercial").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TipoServico).HasColumnName("Id_TipoServico").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.OcorrenciaTransportador).HasColumnName("Id_OcorrenciaTransportador").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ArquivoOcorrenciaConfig).HasColumnName("Id_ArquivoOcorrenciaConfig").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.FatorCubagem).HasColumnName("Nr_FatorCubagem").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.PrazoComercialRelativo).HasColumnName("Nr_PrazoComercialRelativo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.PrazoEntrega).HasColumnName("Nr_PrazoEntrega").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaTransportadorConfigTipoPrazoCliente).HasColumnName("Id_EmpresaTransportadorConfigTipoPrazoCliente").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.OrigemImportacaoId).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint");
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.MostraOcoComplementar).HasColumnName("Flg_MostraOcoComplementar").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

            builder.HasOne(d => d.Empresa)
                  .WithMany(p => p.EmpresaTransportadorConfigs)
                  .HasForeignKey(d => d.EmpresaId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
