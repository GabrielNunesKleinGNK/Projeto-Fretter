using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaStageRemetenteMap : IEntityTypeConfiguration<EntregaStageRemetente>
    {
        public void Configure(EntityTypeBuilder<EntregaStageRemetente> builder)
        {
            builder.ToTable("Tb_Edi_EntregaStageRemetente", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EntregaStage).HasColumnName("Id_EntregaStage").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.CodigoIntegracao).HasColumnName("Cd_CodigoIntegracao").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.Cep).HasColumnName("Cd_Cep").HasColumnType("varchar").HasMaxLength(8);
			builder.Property(e => e.Endereco).HasColumnName("Ds_Endereco").HasColumnType("varchar").HasMaxLength(256);
			builder.Property(e => e.Numero).HasColumnName("Ds_Numero").HasColumnType("varchar").HasMaxLength(64);
			builder.Property(e => e.Complemento).HasColumnName("Ds_Complemento").HasColumnType("varchar").HasMaxLength(512);
			builder.Property(e => e.Bairro).HasColumnName("Ds_Bairro").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.Cidade).HasColumnName("Ds_Cidade").HasColumnType("varchar").HasMaxLength(128);
			builder.Property(e => e.UF).HasColumnName("Cd_UF").HasColumnType("varchar").HasMaxLength(16);
			builder.Property(e => e.Alteracao).HasColumnName("Dt_Alteracao").HasColumnType("datetime").HasMaxLength(8);
			builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit").HasMaxLength(1);
        }
    }
}
