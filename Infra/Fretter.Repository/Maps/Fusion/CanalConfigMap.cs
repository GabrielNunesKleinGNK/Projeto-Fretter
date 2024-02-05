using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CanalConfigMap : IEntityTypeConfiguration<CanalConfig>
    {
        public void Configure(EntityTypeBuilder<CanalConfig> builder)
        {
            builder.ToTable("Tb_MF_CanalConfig", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Id_Canal").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.ControlaSaldo).HasColumnName("Flg_ControlaSaldo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.Ativo);            

            builder.HasOne(d => d.Empresa)
                  .WithMany(p => p.CanalConfigs)
                  .HasForeignKey(d => d.EmpresaId)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
