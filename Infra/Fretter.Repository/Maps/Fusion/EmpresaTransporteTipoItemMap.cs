using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaTransporteTipoItemMap : IEntityTypeConfiguration<EmpresaTransporteTipoItem>
    {
        public void Configure(EntityTypeBuilder<EmpresaTransporteTipoItem> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaTransporteTipoItem", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EmpresaTransporteTipoId).HasColumnName("Id_EmpresaTransporteTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Url).HasColumnName("Ds_Url").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Alias).HasColumnName("Cd_Alias").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);


            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

            builder.HasOne(d => d.EmpresaTransporteTipo).WithMany().HasForeignKey(d => d.EmpresaTransporteTipoId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(d => d.Transportador).WithMany().HasForeignKey(d => d.TransportadorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(d => d.EmpresaTransporteConfiguracoes).WithOne(x => x.EmpresaTransporteTipoItem).HasForeignKey(d => d.EmpresaTransporteTipoItemId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
