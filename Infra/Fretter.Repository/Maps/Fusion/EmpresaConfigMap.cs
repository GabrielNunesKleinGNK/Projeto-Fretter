using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaConfigMap : IEntityTypeConfiguration<EmpresaConfig>
    {
        public void Configure(EntityTypeBuilder<EmpresaConfig> builder)
        {
            builder.ToTable("Tb_MF_EmpresaConfig", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.NaoUsaEndpointExterno).HasColumnName("Flg_NaoUsaEndpointExterno").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.SplitCanal).HasColumnName("Fl_SplitCanal").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.ControlaSaldoProdutos).HasColumnName("Flg_ControlaSaldoProdutos").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.QtdTabelas).HasColumnName("Nr_QtdTabelas").HasColumnType("int").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(d => d.Empresa)
                  .WithMany(p => p.EmpresaConfigs)
                  .HasForeignKey(d => d.Id)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
