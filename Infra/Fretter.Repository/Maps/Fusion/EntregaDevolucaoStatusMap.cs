using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaDevolucaoStatusMap : IEntityTypeConfiguration<EntregaDevolucaoStatus>
    {
        public void Configure(EntityTypeBuilder<EntregaDevolucaoStatus> builder)
        {
            builder.ToTable("Tb_Edi_EntregaDevolucaoStatus", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaTransporteTipoId).HasColumnName("Id_EntregaTransporteTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(16);
            builder.Property(e => e.Alias).HasColumnName("Cd_Alias").HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.MonitoraOcorrencia).HasColumnName("Flg_MonitoraOcorrencia").HasColumnType("bit").HasMaxLength(1);            
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
