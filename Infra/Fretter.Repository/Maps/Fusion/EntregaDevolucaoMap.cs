using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaDevolucaoMap : IEntityTypeConfiguration<EntregaDevolucao>
    {
        public void Configure(EntityTypeBuilder<EntregaDevolucao> builder)
        {
            builder.ToTable("Tb_Edi_EntregaDevolucao", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaId).HasColumnName("Id_Entrega").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaTransporteTipoId).HasColumnName("Id_EntregaTransporteTipo").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.CodigoColeta).HasColumnName("Cd_CodigoColeta").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoRastreio).HasColumnName("Cd_CodigoRastreio").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.CodigoEntregaSaidaItem).HasColumnName("Cd_EntregaSaidaItem").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Validade).HasColumnName("Dt_Validade").HasColumnType("date").HasMaxLength(3);
            builder.Property(e => e.Observacao).HasColumnName("Ds_Observacao").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("Id_UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataAlteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("Id_UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.DataUltimaOcorrencia).HasColumnName("Dt_UltimaOcorrencia").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UltimaOcorrenciaCodigo).HasColumnName("Ds_UltimaOcorrenciaCodigo").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.UltimaOcorrencia).HasColumnName("Ds_UltimaOcorrencia").HasColumnType("varchar").HasMaxLength(512);
            builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.EntregaDevolucaoStatus).HasColumnName("Id_EntregaDevolucaoStatus").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Processado).HasColumnName("Flg_Processado").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Finalizado).HasColumnName("Flg_Finalizado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.CodigoRetorno).HasColumnName("Cd_CodigoRetorno").HasColumnType("varchar").HasMaxLength(32);
            builder.Property(e => e.EntregaReversaId).HasColumnName("Id_EntregaReversa").HasColumnType("int").HasMaxLength(4);

            builder.HasOne(e => e.Status).WithMany(x => x.EntregasDevolucoes).HasForeignKey(e => e.EntregaDevolucaoStatus).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.Ocorrencias).WithOne().HasForeignKey(e => e.EntregaDevolucao).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Entrega).WithMany().HasForeignKey(e => e.EntregaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.EntregaReversa).WithMany().HasForeignKey(e => e.EntregaReversaId).OnDelete(DeleteBehavior.Restrict);


            builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}
