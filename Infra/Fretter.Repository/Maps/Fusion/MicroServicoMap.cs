using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class MicroServicoMap : IEntityTypeConfiguration<MicroServico>
    {
        public void Configure(EntityTypeBuilder<MicroServico> builder)
        {
            builder.ToTable("Tb_Adm_MicroServico", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaTransportadorId).HasColumnName("Id_EmpresaTransportador").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ServicoCodigo).HasColumnName("Cd_Servico").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.OrigemAWBEmbarcador).HasColumnName("Id_OrigemAWBEmbarcador").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.FormatoEtiqueta).HasColumnName("Id_FormatoEtiqueta").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.GeraEtiquetaFusion).HasColumnName("Flg_GeraEtiquetaFusion").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Descricao).HasColumnName("Ds_Descricao").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.DimensoesPesoObg).HasColumnName("Flg_DimensoesPesoObg").HasColumnType("bit").HasMaxLength(1);
            //builder.Property(e => e.ConfigExpedicaoCompleta).HasColumnName("Flg_ConfigExpedicaoCompleta").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ModalidadeTransportador).HasColumnName("Ds_ModalidadeTransportador").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.DadosDestinatarioObg).HasColumnName("Flg_DadosDestinatarioObg").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Servico2).HasColumnName("Cd_Servico2").HasColumnType("varchar").HasMaxLength(2);
            builder.Property(e => e.PrazoAutomatico).HasColumnName("Fl_PrazoAutomatico").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.ServicoDescricao).HasColumnName("Ds_Servico").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            

            //Relacionamento por Coluna que não é a PK
            builder.HasOne(d => d.EmpresaTransportadorConfig)
                  .WithMany(p => p.MicroServicos)
                  //.HasPrincipalKey(p => p.TransportadorId)
                  .HasForeignKey(d => d.EmpresaTransportadorId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
