using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CanalVendaConfigMap : IEntityTypeConfiguration<CanalVendaConfig>
    {
        public void Configure(EntityTypeBuilder<CanalVendaConfig> builder)
        {
            builder.ToTable("Tb_MF_CanalVendaConfig", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id_CanalVenda").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TipoInterface).HasColumnName("Id_TipoInterface").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.EmbalagemUnicaMF).HasColumnName("Fl_EmbalagemUnicaMF").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.TimeoutWS).HasColumnName("Nr_TimeoutWS").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsaMicroServicoNoTransportador).HasColumnName("Fl_UsaMicroServicoNoTransportador").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DescricaoTransportador).HasColumnName("Id_DescricaoTransportador").HasColumnType("tinyint").HasMaxLength(1);
            builder.Property(e => e.PrazoAutomatico).HasColumnName("Fl_PrazoAutomatico").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaToken).HasColumnName("Id_EmpresaToken").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.RetornoDesmembradoMF).HasColumnName("Fl_RetornoDesmembradoMF").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);

            builder.HasOne(d => d.CanalVenda)
                  .WithMany(p => p.CanalVendaConfigs)
                  .HasForeignKey(d => d.Id)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
