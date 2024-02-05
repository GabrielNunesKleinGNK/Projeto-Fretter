using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class OcorrenciaTransportadorTipoMap : IEntityTypeConfiguration<OcorrenciaTransportadorTipo>
    {
        public void Configure(EntityTypeBuilder<OcorrenciaTransportadorTipo> builder)
        {
            builder.ToTable("Tb_Edi_OcorrenciaTransportadorTipo", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Sigla).HasColumnName("Ds_Sigla").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.OcorrenciaTipoId).HasColumnName("Id_OcorrenciaTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);


            builder.HasOne(e => e.OcorrenciaTipo).WithMany().HasForeignKey(e => e.OcorrenciaTipoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Transportador).WithMany().HasForeignKey(e => e.TransportadorId).OnDelete(DeleteBehavior.Restrict);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);

        }
    }
}
