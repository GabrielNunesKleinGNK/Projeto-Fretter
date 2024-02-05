using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageLogMap : IEntityTypeConfiguration<EntregaStageLog>
    {
        public void Configure(EntityTypeBuilder<EntregaStageLog> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageLog", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("bigint").HasMaxLength(8);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Hash).HasColumnName("Ds_Hash").HasColumnType("uniqueidentifier").HasMaxLength(16);
			builder.Property(e => e.Log).HasColumnName("Ds_Log").HasColumnType("varchar").HasMaxLength(4096);
			builder.Property(e => e.Exception).HasColumnName("Ds_Exception").HasColumnType("varchar").HasMaxLength(4096);
			builder.Property(e => e.Complemento).HasColumnName("Ds_Complemento").HasColumnType("varchar").HasMaxLength(1024);
			builder.Property(e => e.Referencia).HasColumnName("Ds_Referencia").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.IP).HasColumnName("Ds_IP").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.URL).HasColumnName("Ds_URL").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.Verbo).HasColumnName("Ds_Verbo").HasColumnType("varchar").HasMaxLength(16);
			builder.Property(e => e.Requisicao).HasColumnName("Ds_Requisicao").HasColumnType("varchar").HasMaxLength(2048);
			builder.Property(e => e.UsuarioCadastro).HasColumnName("Cd_UsuarioCadastro").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
        }
    }
}
