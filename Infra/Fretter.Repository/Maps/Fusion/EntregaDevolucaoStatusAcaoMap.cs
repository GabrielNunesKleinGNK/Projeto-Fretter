using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaDevolucaoStatusAcaosMap : IEntityTypeConfiguration<EntregaDevolucaoStatusAcao>
    {
        public void Configure(EntityTypeBuilder<EntregaDevolucaoStatusAcao> builder)
        {
            builder.ToTable("Tb_Edi_EntregaDevolucaoStatusAcao", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaTransporteTipoId).HasColumnName("Id_EntregaTransporteTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaDevolucaoStatusId).HasColumnName("Id_EntregaDevolucaoStatus").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaDevolucaoAcaoId).HasColumnName("Id_EntregaDevolucaoAcao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.EntregaDevolucaoResultadoId).HasColumnName("Id_EntregaDevolucaoResultado").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Visivel).HasColumnName("Flg_Visivel").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.InformaMotivo).HasColumnName("Flg_InformaMotivo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);

            builder.HasOne(e => e.Acao).WithMany().HasForeignKey(e => e.EntregaDevolucaoAcaoId).OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
